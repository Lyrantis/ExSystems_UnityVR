using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Puzzle> puzzles = new List<Puzzle>();

    public string endCombination;

    [SerializeField]
    public List<Material> colourMaterials = new List<Material>();
    private List<Material> coloursUsed= new List<Material>();

    private Color[] colours = { Color.red, Color.green, Color.blue, Color.magenta, Color.yellow, Color.black };

    private int[] clueColours = new int[4];

    [SerializeField]
    TMP_Text puzzleTrackingText;

    private int puzzlesToComplete;
    private int puzzlesCompleted;

    [SerializeField]
    public float Timer = 180.0f;


    private void Start()
    {
        endCombination = Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString() + Random.Range(0, 10).ToString();

        for (int i = 0; i < 4; i++)
        {
            int randomColour = UnityEngine.Random.Range(0, colourMaterials.Count);

            while (clueColours.Contains(randomColour))
            {
                randomColour = UnityEngine.Random.Range(0, colourMaterials.Count);
            }
            clueColours[i] = randomColour;
            coloursUsed.Add(colourMaterials[randomColour]);
        }

        FindAnyObjectByType<ColourBrick>().SetColours(coloursUsed);

        Puzzle[] puzzlesInWorld = FindObjectsOfType<Puzzle>();

        foreach (Puzzle puzzle in puzzlesInWorld) 
        { 
            puzzles.Add(puzzle);
            puzzle.OnCompleted += PuzzleSolved;
        }
        puzzlesToComplete = puzzles.Count;

        for (int i = 0; i < 4; i++)
        {
            puzzles[i].SetClueValues(colours[clueColours[i]], endCombination[i]);
        }

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
