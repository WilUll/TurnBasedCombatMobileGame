using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUDScript : MonoBehaviour
{
    public GameObject player;

    public TMP_Text playerName;
    public TMP_Text level;
    public Slider hpSlider;


    private void Start()
    {
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        yield return new WaitForSeconds(0.2f);
        if (player.GetComponent<PlayerInfo>() == null)
        {
            OpponentInfo oppInfo = player.GetComponent<OpponentInfo>();
            playerName.text = oppInfo.Name;
            level.text = oppInfo.level.ToString();
            hpSlider.maxValue = oppInfo.maxHealth;
            hpSlider.value = oppInfo.currentHealth;

        }
        else if (player.GetComponent<PlayerInfo>() != null)
        {
            PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
            playerName.text = playerInfo.Name;
            level.text = playerInfo.level.ToString();
            hpSlider.maxValue = playerInfo.maxHealth;
            hpSlider.value = playerInfo.currentHealth;

        }
    }

    public void UpdateHUD()
    {
        if (player.GetComponent<PlayerInfo>() == null)
        {
            OpponentInfo oppInfo = player.GetComponent<OpponentInfo>();
            hpSlider.value = oppInfo.currentHealth;

        }
        else if (player.GetComponent<PlayerInfo>() != null)
        {
            PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
            hpSlider.value = playerInfo.currentHealth;
        }
    }

}
