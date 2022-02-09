using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYER1TURN, PLAYER2TURN, END }

public class BattleSystem : MonoBehaviour
{
    public BattleState state;

    public int player1AttackChoice;
    public int player1Damage;
    public int player2AttackChoice;
    public int player2Damage;

    Abilities attacks;
    public GameObject attackButtonsHUD;

    public GameObject player1;
    public GameObject player2;

    public BattleHUDScript player1HUD;
    public BattleHUDScript player2HUD;
    void Start()
    {
        state = BattleState.START;
        attacks = GetComponent<Abilities>();

        BattleSetup();
    }

    private void BattleSetup()
    {
        Player1Turn();
        //TODO: ASSIGN PLAYER AS p1 or p2




        //int randomizeWhoStarts = Random.Range(0, 2);

        //if (randomizeWhoStarts == 0)
        //{
        //    state = BattleState.PLAYER1TURN;
        //    Player1Turn();
        //}
        //else
        //{
        //    state = BattleState.PLAYER2TURN;
        //    Player2Turn();
        //}
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
            AttackButtons(2);
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
                player2.GetComponent<OpponentInfo>().TakeDamage(player1.GetComponent<PlayerInfo>().damage);
                player2HUD.UpdateHUD();
                if (player2.GetComponent<OpponentInfo>().currentHealth <= 0)
                {
                    EndCard();
                }
            }
            else if (attacks.attackRules[player2AttackChoice] == player1AttackChoice)
            {
                player1.GetComponent<PlayerInfo>().TakeDamage(player2.GetComponent<OpponentInfo>().damage);
                player1HUD.UpdateHUD();
                if (player1.GetComponent<PlayerInfo>().currentHealth <= 0)
                {
                    player1.GetComponent<PlayerInfo>().Exp += 50;
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
        state = BattleState.END;

        SceneManager.LoadScene("GameView");
    }


    private void ResetCombatVariables()
    {
        player1AttackChoice = 0;
        player1Damage = 0;
        player2AttackChoice = 0;
        player2Damage = 0;
    }
}
