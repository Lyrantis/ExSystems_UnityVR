using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private bool isCompleted = false;

    [SerializeField]
    TMP_Text clue;

    private Color clueColour;
    private int clueNum;
    public event Action<Puzzle> OnCompleted;

    public void SetClueValues(Color inColour, int inNum)
    {
        clueColour = inColour;
        clueNum = inNum;
        clue.color = clueColour;
        clue.text = String.Empty;
    }

    public void OnPuzzleCompleted()
    {
        if (!isCompleted)
        {
            clue.text = clueNum.ToString();
            isCompleted = true;
            OnCompleted?.Invoke(this);
        }   
    }
}
