using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameRule : MonoBehaviour
{
    public UnityEvent<GameRule, int> OnCompleted;
    public int m_pointsValue = 100;

    private void Start()
    {
        if (OnCompleted == null)
        {
            OnCompleted = new UnityEvent<GameRule, int>();
        }
    }

    public void OnRuleCompleted()
    {
        OnCompleted.Invoke(this, m_pointsValue);
    }
}
