using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Numpad : MonoBehaviour
{
    private string currentGuess = "";
    private string correctCombination;
    private int codeNum;
    private bool solved = false;

    [SerializeField]
    TMP_Text guessDisplay;

    [SerializeField]
    GameObject endBlockage;

    public event Action OnSolved;

    IEnumerator WaitToInit()
    {
        yield return new WaitForSeconds(0.1f);
        correctCombination = FindObjectOfType<GameManager>().endCombination;
        codeNum = correctCombination.Length;
    }
    private void Start()
    {
        StartCoroutine(WaitToInit());      
    }

    public void AddGuessDigit(int guess)
    {
        if (!solved) 
        {
            currentGuess += guess.ToString();
            if (currentGuess.Length >= codeNum)
            {
                if (currentGuess == correctCombination)
                {
                    Destroy(endBlockage);

                    solved = true;
                    guessDisplay.text = "OPEN";
                    OnSolved?.Invoke();
                    Destroy(this);
                }
                else
                {
                    Debug.Log("WRONG");
                    currentGuess = "";
                }
            }
            guessDisplay.text = currentGuess;
        }
    }
}
