using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Puzzle> puzzles = new List<Puzzle>();

    public string endCombination;

    [SerializeField]
    TMP_Text puzzleTrackingText;

    private int puzzlesToComplete;
    private int puzzlesCompleted;

    [SerializeField]
    public float Timer = 180.0f;


    private void Start()
    {
        Puzzle[] puzzlesInWorld = FindObjectsOfType<Puzzle>();

        foreach (Puzzle puzzle in puzzlesInWorld) 
        { 
            puzzles.Add(puzzle);
            puzzle.OnCompleted += PuzzleSolved;
        }
        puzzlesToComplete = puzzles.Count;
        endCombination = Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString();

        puzzleTrackingText.text = "Puzzles Solved: " + puzzlesCompleted + "/" + puzzlesToComplete;
    }

    private void PuzzleSolved(Puzzle puzzleCompleted)
    {
        puzzles.Remove(puzzleCompleted);
        puzzlesCompleted++;

        puzzleTrackingText.text = "Puzzles Solved: " + puzzlesCompleted + "/" + puzzlesToComplete;

        if (puzzlesCompleted >= puzzlesToComplete)
        {
            Debug.Log("You win! Or something");
        }

    }
}
