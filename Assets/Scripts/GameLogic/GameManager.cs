using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<GameRule> rules;
    private int ruleNum;

    private void Start()
    {
        GameRule[] newrules = gameObject.GetComponents<GameRule>();

        foreach (GameRule rule in newrules)
        {
            rules.Add(rule);
            rule.OnCompleted.AddListener(GameRuleCompleted);
        }

        ruleNum = rules.Count;
    }

    private void GameRuleCompleted(GameRule rule, int points)
    {
        ruleNum--;
        rules.Remove(rule);

        if (ruleNum == 0)
        {
            //Spawn Key for final door 
        }
    }
}
