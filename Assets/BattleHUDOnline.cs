using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUDOnline : MonoBehaviour
{
    public Slider player1Health;
    public Slider player2Health;
    public TMP_Text player1Name;
    public TMP_Text player2Name;

    public GameObject panel;
    public TMP_Text panelText;

    public BattleSystemOnline battleSystem;
    public void UpdateHud()
    {
        player1Health.maxValue = battleSystem.player1MaxHealth;
        player2Health.maxValue = battleSystem.player2MaxHealth;

        player1Health.value = battleSystem.player1CurrentHealth;
        player2Health.value = battleSystem.player2CurrentHealth;

        player1Name.text = BattleSystemOnline.gameSession.players[0].Name;
        player2Name.text = BattleSystemOnline.gameSession.players[1].Name;
    }

    public void ActivatePanel(string topText)
    {
        panel.SetActive(true);
        panelText.text = topText;
    }
}
