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

        int AiDifficulty = Random.Range(0, 10);

        if (AiDifficulty <= 5)
        {
            aiPlayerScript.Name = "Easy Ai";
            aiPlayerScript.level = userPlayerScript.level + Random.Range(-5, 5);
        }
        else if (AiDifficulty > 5 && AiDifficulty < 8)
        {
            aiPlayerScript.Name = "Medium Ai";
            aiPlayerScript.level = userPlayerScript.level + Random.Range(0, 8);

        }
        else if (AiDifficulty >= 8)
        {
            aiPlayerScript.Name = "Boss";
            aiPlayerScript.level = userPlayerScript.level + Random.Range(5, 10);
        }

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
