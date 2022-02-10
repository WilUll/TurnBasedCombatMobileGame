using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentInfoBattleMode : MonoBehaviour
{
    PlayerInfo userPlayerScript;
    PlayerInfo aiPlayerScript;

    // Start is called before the first frame update
    void Start()
    {
        userPlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        aiPlayerScript = GetComponent<PlayerInfo>();
        aiPlayerScript.level = userPlayerScript.level + Random.Range(-5, 5);
        if (aiPlayerScript.level <= 0)
        {
            aiPlayerScript.level = 1;
        }
        CalculateVariables();
    }

    public void CalculateVariables()
    {
        aiPlayerScript.maxHealth = 100 + (aiPlayerScript.level * 5);
        aiPlayerScript.currentHealth = aiPlayerScript.maxHealth;
        aiPlayerScript.damage = 10 + (aiPlayerScript.level * 1);
    }
}
