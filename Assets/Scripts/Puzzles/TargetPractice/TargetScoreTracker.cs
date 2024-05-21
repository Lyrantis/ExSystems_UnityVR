using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScoreTracker : MonoBehaviour
{
    private int Score = 0;
    public int ScoreToReach = 0;
    public void AddScore(int TargetPoints)
    {
        Score += TargetPoints;

    }

    public void Start()
    {
        Score = 0;
    }

    public bool EndGame()
    {
        if (Score >= ScoreToReach)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
