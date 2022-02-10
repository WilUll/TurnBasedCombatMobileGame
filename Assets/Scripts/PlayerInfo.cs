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

    public delegate void DamageTaken();
    public static event DamageTaken OnDamage;
    // Start is called before the first frame update
    void Awake()
    {


        if (Name == "")
        {
            Name = "AI";
        }
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

        if (OnDamage != null)
            OnDamage();
    }
}
