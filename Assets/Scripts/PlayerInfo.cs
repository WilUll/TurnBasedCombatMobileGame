using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInfo : MonoBehaviour
{
    public string Name;
    public float ColorHUE;
    public int level;
    public float Exp;
    public float maxHealth;
    public float currentHealth;
    public float damage;

    // Start is called before the first frame update
    void Start()
    {
        //Get the saved jsonString
        string jsonString = PlayerPrefs.GetString("PlayerSaveData");

        //Convert the data to a object


        PlayerSaveData loadedData = JsonUtility.FromJson<PlayerSaveData>(jsonString);
        Debug.Log("Loaded data");


        Name = loadedData.Name;
        ColorHUE = loadedData.ColorHUE;
        Exp = loadedData.Exp;

        gameObject.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(ColorHUE, 0.85f, 0.85f);
        Debug.Log("start");

        CalculateVariables();
    }

    void CalculateVariables()
    {
        float expVar = Mathf.FloorToInt(Exp / 100);
        if (expVar == 0)
        {
            expVar = 1;
        }
        Debug.Log("start2");
        level = (int)expVar;
        maxHealth = 100 + (level * 5);
        currentHealth = maxHealth;
        damage = 10 + (level * 1);
    }

    public void TakeDamage(float damageToDeal)
    {
        currentHealth -= damageToDeal;
    }
}
