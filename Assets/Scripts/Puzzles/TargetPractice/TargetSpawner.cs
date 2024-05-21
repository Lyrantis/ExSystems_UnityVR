using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    
    [SerializeField]
    GameObject movingTarget;

    [SerializeField]
    List<GameObject> targetSpawns = new List<GameObject>();
    
    [SerializeField]
    List<GameObject> movingTargetSpawns = new List<GameObject>();

    [SerializeField]
    float movingChance = 0.2f;

    private List<GameObject> activeTargets = new List<GameObject>();

    public event Action<int> OnPointsEarned;

    IEnumerator WaitBetweenTargetSpawn()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        float movingOdds = UnityEngine.Random.Range(0.0f, 1.0f);
        GameObject temp;

        if (movingOdds <= movingChance)
        {
            temp = Instantiate(movingTarget, movingTargetSpawns[UnityEngine.Random.Range(0, movingTargetSpawns.Count)].transform);
            temp.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        else
        {
            temp = Instantiate(target, targetSpawns[UnityEngine.Random.Range(0, targetSpawns.Count)].transform);
        }

        activeTargets.Add(temp);
        temp.GetComponent<Target>().OnDestroyed += OnTargetShot;
        StartCoroutine(WaitBetweenTargetSpawn());
    }

    private void OnTargetShot(GameObject targetShot, int points)
    {
        activeTargets.Remove(targetShot);
        OnPointsEarned.Invoke(points);
    }

    public void StartGame()
    {
        SpawnTarget();
    }

    public void EndGame()
    {
        StopAllCoroutines();

        foreach (GameObject target in activeTargets) 
        {
            Destroy(target);
        }
    }
}
