using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Auth;

public class PlayerInfo : MonoBehaviour
{
    public string Name;
    public float ColorHUE;
    public int level;
    public float Exp;
    public float maxHealth;
    public float currentHealth;
    public float damage;
    public int WinStreak;

    public delegate void DamageTaken();
    public static event DamageTaken OnDamage;

    // Start is called before the first frame update
    void Awake()
    {
        if (gameObject.tag != "Player") return;


        CalculateVariables();
    }

    void SetPlayerValues()
    {
        Name = PlayerData.data.Name;
        ColorHUE = PlayerData.data.ColorHUE;
        level = PlayerData.data.Level;
        Exp = PlayerData.data.Exp;
        WinStreak = PlayerData.data.WinStreak;
    }

    void CalculateVariables()
    {
        maxHealth = 100 + (level * 5);
        currentHealth = maxHealth;
        damage = 10 + (level * 1);
    }

    public void TakeDamage(float damageToDeal)
    {
        currentHealth -= damageToDeal;

        OnDamage?.Invoke();
    }



    //Level calculated by (level + 1) * 100
    public void AddXP(float xp)
    {
        Exp += xp;
        if (Exp >= ((level + 1) * 100))
        {
            Exp -= ((level + 1) * 100);
            level++;
        }
    }


}
