using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Puzzle> puzzles = new List<Puzzle>();

    private string endCombination;

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
    }

    private void PuzzleSolved(Puzzle puzzleCompleted)
    {
        puzzles.Remove(puzzleCompleted);
        puzzlesCompleted++;


    }
}
