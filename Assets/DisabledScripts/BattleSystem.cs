using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYER1TURN, PLAYER2TURN, PLAYER1WON, PLAYER2WON }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public int player1AttackChoice;
    public int player2AttackChoice;

    Abilities attacks;
    public GameObject attackButtonsHUD;

    public PlayerInfo player1Script;
    public PlayerInfo player2Script;

    public BattleHUDScript player1HUD;
    public BattleHUDScript player2HUD;

    public GameObject endPanel;
    void Start()
    {
        state = BattleState.START;
        attacks = GetComponent<Abilities>();

        BattleSetup();
    }

    private void BattleSetup()
    {
        Player1Turn();
        //TODO: ASSIGN players 1 or 2
        //player 1 will be the creator of the game and 2 will be the person joinging

    }

    private void Player1Turn()
    {
        state = BattleState.PLAYER1TURN;
        attackButtonsHUD.SetActive(true);
    }

    private void Player2Turn()
    {
        state = BattleState.PLAYER2TURN;
        attackButtonsHUD.SetActive(false);
        StartCoroutine(WaitForAttack());
        IEnumerator WaitForAttack()
        {
            yield return new WaitForSeconds(0.5f);
            AttackButtons(Random.Range(1,4));
        }
    }

    public void AttackButtons(int attack)
    {
        if (state == BattleState.PLAYER1TURN)
        {
            player1AttackChoice = attack;
        }
        else
        {
            player2AttackChoice = attack;
        }
        CheckWhoWon();
    }

    private void CheckWhoWon()
    {
        if (player2AttackChoice != 0 && player1AttackChoice != 0)
        {
            if (attacks.attackRules[player1AttackChoice] == player2AttackChoice)
            {
                player1Script.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger(player1AttackChoice.ToString());
                player2Script.TakeDamage(10);
                if (player2Script.currentHealth <= 0)
                {
                    state = BattleState.PLAYER1WON;

                    EndCard();
                }
            }
            else if (attacks.attackRules[player2AttackChoice] == player1AttackChoice)
            {
                player2Script.gameObject.gameObject.transform.GetChild(0).GetComponent<Animator>().SetTrigger(player2AttackChoice.ToString());
                player1Script.TakeDamage(10);
                if (player1Script.currentHealth <= 0)
                {
                    state = BattleState.PLAYER2WON;

                    EndCard();
                }
            }
            else
            {
                Debug.Log("Draw");
            }
            ChangePlayerState();
            ResetCombatVariables();
        }
        else
        {
            ChangePlayerState();
        }
    }

    private void ChangePlayerState()
    {
        if (state == BattleState.PLAYER1TURN)
        {
            Player2Turn();
        }
        else
        {
            Player1Turn();
        }
    }

    private void EndCard()
    {
        if (state == BattleState.PLAYER1WON)
        {
            player1Script.WinStreak++;
        }
        else
        {
            player1Script.WinStreak = 0;
        }
        endPanel.SetActive(true);
    }


    private void ResetCombatVariables()
    {
        player1AttackChoice = 0;
        player2AttackChoice = 0;
    }
}
