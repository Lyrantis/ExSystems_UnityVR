using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private bool isCompleted = false;

    public event Action<Puzzle> OnCompleted;

    private void OnPuzzleCompleted()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            OnCompleted.Invoke(this);
        }   
    }
}
