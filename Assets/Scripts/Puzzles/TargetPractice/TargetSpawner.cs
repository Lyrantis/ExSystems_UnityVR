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

    public void SpawnTarget()
    {
        float movingOdds = UnityEngine.Random.Range(0f, 1f);
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
    }

    private void OnTargetShot(GameObject targetShot, int points)
    {
        OnPointsEarned.Invoke(points);
    }
}
