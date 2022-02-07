using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentInfo : MonoBehaviour
{
    public string Name = "AI";
    public float ColorHUE = 0f;
    public int level;
    public float maxHealth;
    public float currentHealth;
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SaveManager.Instance.SaveOpponent(gameObject);
        }
    }

}
