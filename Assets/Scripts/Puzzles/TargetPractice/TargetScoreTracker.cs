using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetScoreTracker : MonoBehaviour
{
    private int Score = 0;
    public int ScoreToReach = 0;

    [SerializeField]
    public TMP_Text scoreText;

    public void AddScore(int TargetPoints)
    {
        Score += TargetPoints;
        scoreText.text = "Score : " + Score.ToString();

    }

    public void Reset()
    {
        Score = 0;
        scoreText.text = "Score : " + Score.ToString();
    }

    public void Start()
    {
        Reset();
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
