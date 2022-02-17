using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleSystemOnline : MonoBehaviour
{
    public static GameInfo gameSession;
    public GameObject attackButtonsHUD;

    public int player1MaxHealth, player1CurrentHealth, player1Damage;
    public int player2MaxHealth, player2CurrentHealth, player2Damage;

    public GameObject Player1;
    public GameObject Player2;


    Abilities abilities;
    private void Start()
    {
        player1MaxHealth = 100;
        player1CurrentHealth = player1MaxHealth;
        player1Damage = 10;

        player2MaxHealth = 100;
        player2CurrentHealth = player2MaxHealth;
        player2Damage = 10;

        abilities = GetComponent<Abilities>();


        attackButtonsHUD.SetActive(false);

        SaveManager.Instance.LoadData("games/" + PlayerData.data.activeGameID, OnDataLoad);

    }

    private void OnDataLoad(string gameString)
    {
        gameSession = JsonUtility.FromJson<GameInfo>(gameString);
        gameSession.Player1Attack = 0;
        gameSession.Player2Attack = 0;
        GetComponent<FirebaseOnline>().Subscribe(gameSession.gameID);
        if (gameSession.isFull)
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        CheckTurn();
    }

    private void CheckTurn()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser.UserId == gameSession.userIDTurn)
        {
            ActivateButtons();
        }
        else
        {
            attackButtonsHUD.SetActive(false);
        }
    }

    private void ActivateButtons()
    {
        attackButtonsHUD.SetActive(true);
    }

    public void SetAttack(int attack)
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser.UserId == gameSession.Player1ID)
        {
            gameSession.Player1Attack = attack;
        }
        else if (FirebaseAuth.DefaultInstance.CurrentUser.UserId == gameSession.Player2ID)
        {
            gameSession.Player2Attack = attack;
        }
        CheckForAttack();
        ChangePlayerTurn();
    }

    private void CheckForAttack()
    {
        if (gameSession.Player1Attack != 0 && gameSession.Player2Attack != 0)
        {
            if (abilities.attackRules[gameSession.Player1Attack] == gameSession.Player2Attack)
            {
                player2CurrentHealth -= player1Damage;

                if (FirebaseAuth.DefaultInstance.CurrentUser.UserId == gameSession.Player1ID)
                {
                    Player1.transform.GetChild(0).GetComponent<Animator>().SetTrigger(gameSession.Player1Attack.ToString());
                }
                else
                {
                    Player2.transform.GetChild(0).GetComponent<Animator>().SetTrigger(gameSession.Player1Attack.ToString());
                }



                if (player2CurrentHealth <= 0)
                {
                    EndCard();
                }
            }
            else if (abilities.attackRules[gameSession.Player2Attack] == gameSession.Player1Attack)
            {
                player1CurrentHealth -= player2Damage;

                if (FirebaseAuth.DefaultInstance.CurrentUser.UserId == gameSession.Player1ID)
                {
                    Player2.transform.GetChild(0).GetComponent<Animator>().SetTrigger(gameSession.Player2Attack.ToString());
                }
                else
                {
                    Player1.transform.GetChild(0).GetComponent<Animator>().SetTrigger(gameSession.Player2Attack.ToString());
                }

                if (player1CurrentHealth <= 0)
                {
                    EndCard();
                }
            }
            ResetAttacks();
        }
    }

    private void PlayAnimation()
    {

    }

    private void ResetAttacks()
    {
        if (gameSession.userIDTurn == gameSession.Player1ID && FirebaseAuth.DefaultInstance.CurrentUser.UserId == gameSession.Player1ID)
        {
            gameSession.Player1Attack = 0;
            gameSession.Player2Attack = 0;
        }
    }

    public void ChangePlayerTurn()
    {
        if (gameSession.userIDTurn == gameSession.Player1ID)
        {
            gameSession.userIDTurn = gameSession.Player2ID;
        }
        else
        {
            gameSession.userIDTurn = gameSession.Player1ID;
        }
        SaveGame();
        attackButtonsHUD.SetActive(false);
    }

    private void EndCard()
    {
        Debug.Log("End");
    }


    private void SaveGame()
    {
        SaveManager.Instance.SaveData("games/" + gameSession.gameID, JsonUtility.ToJson(gameSession));
    }

    public void RefreshGame(GameInfo newGameState)
    {
        if (newGameState != gameSession)
        {
            gameSession = newGameState;
        }
        if (gameSession.userIDTurn == FirebaseAuth.DefaultInstance.CurrentUser.UserId)
        {
            CheckForAttack();
            StartGame();
        }
    }
}
