using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.ShortcutManagement;
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

    [SerializeField]
    private TMP_Text timeTrackingText;

    private int puzzlesToComplete;
    private int puzzlesCompleted = 0;

    [SerializeField]
    public float Timer = 180.0f;

    [SerializeField]
    GameObject endBlock;

    IEnumerator UpdateTimer()
    {
        yield return new WaitForSeconds(1.0f);
        Timer -= 1.0f;
        timeTrackingText.text = "Time Remaining: " + Timer.ToString();

        if (Timer <= 0.0f)
        {
            //Game Over!
        }
        else
        {
            StartCoroutine(UpdateTimer());
        }
    }

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

        StartCoroutine(UpdateTimer());

        FindObjectOfType<Numpad>().OnSolved += StartEnd;
    }
    public void StartEnd()
    {
        BackToMenu();
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(5.0f);

        gameObject.GetComponent<SceneChanger>().LoadScene(0);
    }

    private void PuzzleSolved(Puzzle puzzleCompleted)
    {
        puzzles.Remove(puzzleCompleted);
        puzzlesCompleted++;

        puzzleTrackingText.text = "Puzzles Solved: " + puzzlesCompleted + "/" + puzzlesToComplete;

        if (puzzlesCompleted >= puzzlesToComplete)
        {
            //Combination Puzzle Unlocks
        }

    }
}
