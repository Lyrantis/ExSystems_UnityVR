using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int ScoreValue = 1;
    public event Action<GameObject, int> OnDestroyed;

    public void Init(int points)
    {
        ScoreValue = points;
        transform.parent = null;

    }

    public void OnHit()
    {
        OnDestroyed.Invoke(gameObject, ScoreValue);
        Destroy(gameObject);
    }
}
