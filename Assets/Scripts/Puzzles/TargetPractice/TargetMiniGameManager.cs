using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMiniGameManager : MonoBehaviour
{
    private TargetScoreTracker scoreTracker;
    private TargetSpawner spawner;


    // Start is called before the first frame update
    void Start()
    {
        scoreTracker = GetComponent<TargetScoreTracker>();
        spawner = GetComponent<TargetSpawner>();
        spawner.OnPointsEarned += AddPoints;
    }

    private void AddPoints(int points)
    {
        scoreTracker.AddScore(points);
    }

    void StartGame()
    {

    }

    void EndGame()
    {

    }
}
