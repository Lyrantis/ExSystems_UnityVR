using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private int ScoreValue = 1;


    public event Action<GameObject, int> OnDestroyed;

    private bool HasBeenHit = false;

    IEnumerator DespawnSelf(float time )
    {
        yield return new WaitForSeconds(time);
        OnDestroyed.Invoke(gameObject, -1);
        Destroy(gameObject);
    }

    private void Start()
    {
    }

    public void Init(float time)
    {

        StartCoroutine(DespawnSelf(time));
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerProjectile" && !HasBeenHit)
        {
            OnHit();
            HasBeenHit = true;
            Destroy(other.gameObject);
        }
    }
}
