using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScoreTracker : MonoBehaviour
{
    private int Score = 0;
    public int ScoreToReach = 0;
    public void TargetDestroyed(int TargetPoints)
    {
        Score += TargetPoints;
        CheckPointsAgainstTarget();
    }

    public void DeductPoints(int PointsToDeduct)
    {
        Score -= PointsToDeduct;
    }

    public void CheckPointsAgainstTarget()
    {
        if (Score >= ScoreToReach)
        {
            //End Game, reveal clue/puzzle component
        }
    }
}
