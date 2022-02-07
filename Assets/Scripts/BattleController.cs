using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum BattleStates { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleController : MonoBehaviour
{
    public BattleStates state;

    public BattleHUDScript playerHUD;
    public BattleHUDScript enemyHUD;
    public GameObject player;
    public GameObject enemy;
    OpponentInfo oppInfo;
    PlayerInfo playInfo;

    void Start()
    {
        oppInfo = enemy.GetComponent<OpponentInfo>();
        playInfo = player.GetComponent<PlayerInfo>();

        state = BattleStates.PLAYERTURN;
    }

    void PlayerTurn()
    {

    }

    public void CloseAttack()
    {
        if (state != BattleStates.PLAYERTURN)
        {
            return;
        }
        else
        {
            CheckWhoWon(0);
        }
    }

    public void RangeAttack()
    {
        if (state != BattleStates.PLAYERTURN)
        {
            return;
        }
        else
        {
            CheckWhoWon(1);
        }
    }

    public void CounterAttack()
    {
        if (state != BattleStates.PLAYERTURN)
        {
            return;
        }
        else
        {
            CheckWhoWon(2);
        }
    }

    void CheckWhoWon(int playerAttack)
    {
        int opponentAttack = Random.Range(0, 4);

        switch (playerAttack)
        {
            case 0:
                if (opponentAttack == 0)
                {
                    DealDamage(0, enemy);
                }
                else if (opponentAttack == 1)
                {
                    DealDamage(playInfo.damage, enemy);
                }
                else
                {
                    DealDamage(oppInfo.damage, player);
                }
                break;

            case 1:
                if (opponentAttack == 0)
                {
                    DealDamage(oppInfo.damage, player);
                }
                else if (opponentAttack == 1)
                {
                    DealDamage(0, enemy);
                }
                else
                {
                    DealDamage(playInfo.damage, enemy);
                }
                break;

            case 2:
                if (opponentAttack == 0)
                {
                    DealDamage(playInfo.damage, enemy);
                }
                else if (opponentAttack == 1)
                {
                    DealDamage(oppInfo.damage, player);
                }
                else
                {
                    DealDamage(0, enemy);
                }
                break;
        }
    }

    void DealDamage(float howMuchDamage, GameObject whoWillTakeDamage)
    {
        if (whoWillTakeDamage.name == "EnemyBattleView")
        {
            oppInfo.currentHealth -= howMuchDamage;
            enemyHUD.GetComponent<BattleHUDScript>().TakeDamage();
            if (oppInfo.currentHealth <= 0)
            {
                playInfo.Exp += 50;
                SaveManager.Instance.PlayerSave(player);
                SceneManager.LoadScene("GameView");
            }
        }
        else
        {
            playInfo.currentHealth -= howMuchDamage;
            playerHUD.GetComponent<BattleHUDScript>().TakeDamage();
            if (playInfo.currentHealth <= 0)
            {
                playInfo.Exp += 25;
                SaveManager.Instance.PlayerSave(player);
                SceneManager.LoadScene("GameView");
            }
        }
    }



}
