using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private int ScoreValue = 1;

    [SerializeField]
    public float timeBeforeDespawn = 5.0f;

    public event Action<GameObject, int> OnDestroyed;

    IEnumerator DespawnSelf()
    {
        yield return new WaitForSeconds(timeBeforeDespawn);
        OnDestroyed.Invoke(gameObject, -1);
        Destroy(gameObject);
    }

    public void Init(int points)
    {
        ScoreValue = points;
        transform.parent = null;
        StartCoroutine(DespawnSelf());
    }

    public void OnHit()
    {
        StopAllCoroutines();
        OnDestroyed.Invoke(gameObject, ScoreValue);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
