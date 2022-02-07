using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentInfoBattleMode : MonoBehaviour
{
    OpponentInfo opponentInfoScript;
    // Start is called before the first frame update
    void Start()
    {
        opponentInfoScript = GetComponent<OpponentInfo>();
        string jsonString = PlayerPrefs.GetString("OpponentSaveData");
        OpponentSaveData loadedData = JsonUtility.FromJson<OpponentSaveData>(jsonString);
        opponentInfoScript.Name = loadedData.Name;
        opponentInfoScript.ColorHUE = loadedData.ColorHUE;
        opponentInfoScript.level = loadedData.level;
        CalculateVariables();
    }

    public void CalculateVariables()
    {
        opponentInfoScript.maxHealth = 100 + (opponentInfoScript.level * 5);
        opponentInfoScript.currentHealth = opponentInfoScript.maxHealth;
        opponentInfoScript.damage = 10 + (opponentInfoScript.level * 1);
    }
}
