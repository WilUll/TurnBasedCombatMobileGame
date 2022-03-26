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
        PlayerInfo.OnDamage += UpdateHUD;
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        yield return new WaitForSeconds(0.2f);
        PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
        playerName.text = playerInfo.Name;
        level.text = playerInfo.level.ToString();
        hpSlider.maxValue = playerInfo.maxHealth;
        hpSlider.value = playerInfo.currentHealth;
    }

    public void UpdateHUD()
    {
        PlayerInfo playerInfo = player.GetComponent<PlayerInfo>();
        hpSlider.value = playerInfo.currentHealth;
    }

    private void OnDisable()
    {
        PlayerInfo.OnDamage -= UpdateHUD;
    }

}
