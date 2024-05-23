using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetMiniGameManager : MonoBehaviour
{
    private TargetScoreTracker scoreTracker;
    private TargetSpawner spawner;

    [SerializeField]
    Button StartButton;

    [SerializeField]
    TMP_Text timerText;

    [SerializeField]
    int timeLimit = 60;

    IEnumerator UpdateTimer()
    {
        for (int i = 0; i < timeLimit + 1; i++)
        {
            yield return new WaitForSeconds(1.0f);
            timerText.text = "Time : " + (timeLimit - i).ToString();
        }
        EndGame();
    }

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

    public void StartGame()
    {
        spawner.StartGame();
        scoreTracker.Reset();

        StartCoroutine(UpdateTimer());
    }

    void EndGame()
    {
        
        spawner.EndGame();
        if (scoreTracker.EndGame())
        {
            Puzzle puzzle = GetComponent<Puzzle>();
            if (puzzle != null)
            {
                puzzle.OnPuzzleCompleted();
            }
            StartButton.GetComponentInChildren<TMP_Text>().text = "Completed!";
            StartButton.image.color = Color.yellow;
        }
        else
        {
            StartButton.interactable = true;
        }
    }
}
