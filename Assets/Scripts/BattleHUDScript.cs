using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUDScript : MonoBehaviour
{
    public OpponentInfo oppInfo;
    public PlayerInfo playerInfo;

    public TMP_Text playerName;
    public TMP_Text level;
    public Slider hpSlider;

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(0.2f);
        if (playerInfo == null)
        {
            playerName.text = oppInfo.Name;
            level.text = oppInfo.level.ToString();
            hpSlider.maxValue = oppInfo.maxHealth;
            hpSlider.value = oppInfo.currentHealth;

        }
        else if (playerInfo != null)
        {
            playerName.text = playerInfo.Name;
            level.text = playerInfo.level.ToString();
            hpSlider.maxValue = playerInfo.maxHealth;
            hpSlider.value = playerInfo.currentHealth;

        }
    }

    public void TakeDamage()
    {
        if (playerInfo == null)
        {
            playerName.text = oppInfo.Name;
            level.text = oppInfo.level.ToString();
            hpSlider.value = oppInfo.currentHealth;

        }
        else if (playerInfo != null)
        {
            playerName.text = playerInfo.Name;
            level.text = playerInfo.level.ToString();
            hpSlider.value = playerInfo.currentHealth;

        }
    }
}
