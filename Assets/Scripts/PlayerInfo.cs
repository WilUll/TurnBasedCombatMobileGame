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

    public delegate void DamageTaken();
    public static event DamageTaken OnDamage;
    // Start is called before the first frame update
    void Awake()
    {
        if (gameObject.tag != "Player") return;
        string userPath = "users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        SaveManager.Instance.LoadData(userPath, SetPlayerValues);



        CalculateVariables();
    }

    void SetPlayerValues(string json)
    {
        PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(json);

        Name = data.Name;
        ColorHUE = data.ColorHUE;
        level = data.Level;
        Exp = data.Exp;
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

        if (OnDamage != null)
            OnDamage();
    }
}
