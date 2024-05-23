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
    float movingChance = -0.2f;

    [SerializeField]
    int maxTargets = 10;

    private List<GameObject> activeTargets = new List<GameObject>();

    public event Action<int> OnPointsEarned;

    IEnumerator WaitBetweenTargetSpawn()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));
        SpawnTarget();
    }

    public void SpawnTarget()
    {
        if ( activeTargets.Count < maxTargets )
        {
            float movingOdds = UnityEngine.Random.Range(0.0f, 1.0f);
            GameObject temp;

            int randomIndex = UnityEngine.Random.Range(0, targetSpawns.Count - 1);
            if (movingOdds <= movingChance)
            {
                temp = Instantiate(movingTarget, targetSpawns[randomIndex].transform.position, targetSpawns[randomIndex].transform.rotation, null);
                if (randomIndex % 2 == 0)
                {
                    temp.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, -5.0f);
                }
                else
                {
                    temp.GetComponent<Rigidbody>().velocity = new Vector3(0.0f, 0.0f, 5.0f);
                }

                temp.GetComponent<Target>().Init(Mathf.Abs(targetSpawns[randomIndex].transform.position.z - targetSpawns[randomIndex + 1].transform.position.z) / temp.GetComponent<Rigidbody>().velocity.magnitude);


            }
            else
            {
                if (randomIndex % 2 != 0)
                {
                    randomIndex--;
                }
                float offset = UnityEngine.Random.Range(0.0f, targetSpawns[randomIndex].transform.position.z - targetSpawns[randomIndex + 1].transform.position.z);
                Vector3 spawnPos = targetSpawns[randomIndex].transform.position;
                spawnPos.z -= offset;
                temp = Instantiate(target, spawnPos, targetSpawns[randomIndex].transform.rotation, null);
            }

            activeTargets.Add(temp);
            temp.GetComponent<Target>().OnDestroyed += OnTargetShot;
        } 
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
            target.GetComponent<Target>().OnDestroyed -= OnTargetShot;
            Destroy(target);
        }
        activeTargets.Clear();
    }
}
