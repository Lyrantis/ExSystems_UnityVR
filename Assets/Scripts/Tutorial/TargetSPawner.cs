using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSPawner : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Transform targetSpawnLocation;

    private float maxSpawnDist = 1.0f;


    public void SpawnTarget()
    {
        Vector3 offset = new Vector3(UnityEngine.Random.Range(-maxSpawnDist, maxSpawnDist), UnityEngine.Random.Range(-maxSpawnDist, maxSpawnDist), UnityEngine.Random.Range(-maxSpawnDist, maxSpawnDist));
        Instantiate(target, targetSpawnLocation.position + offset, targetSpawnLocation.rotation).GetComponent<Target>().OnDestroyed += NewSpawn;
    }

    public void NewSpawn(GameObject target, int score)
    {
        SpawnTarget();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnTarget();
    }
}
