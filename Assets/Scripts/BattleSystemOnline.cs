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

    public BattleHUDOnline hudScript;

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
        Player1.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(PlayerData.data.ColorHUE, 0.85f, 0.85f);
    }

    private void OnDataLoad(string gameString)
    {
        gameSession = JsonUtility.FromJson<GameInfo>(gameString);
        gameSession.Player1Attack = 0;
        gameSession.Player2Attack = 0;
        GetComponent<FirebaseOnline>().Subscribe(gameSession.gameID);
        if (gameSession.isFull)
        {
            SetColors();
            StartGame();
        }
    }

    private void SetColors()
    {
        if (gameSession.Player1ID == FirebaseAuth.DefaultInstance.CurrentUser.UserId)
        {
            Player1.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(PlayerData.data.ColorHUE, 0.85f, 0.85f);
            Player2.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(gameSession.players[1].ColorHUE, 0.85f, 0.85f);

        }
        else
        {
            Player1.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(PlayerData.data.ColorHUE, 0.85f, 0.85f);
            Player2.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(gameSession.players[0].ColorHUE, 0.85f, 0.85f);
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
                    EndCard(gameSession.Player1ID);
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
                    EndCard(gameSession.Player2ID);
                }
            }
            ResetAttacks();
        }
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

    private void EndCard(string winnerID)
    {
        if (winnerID == FirebaseAuth.DefaultInstance.CurrentUser.UserId)
        {
            hudScript.ActivatePanel("You Won!");
            PlayerData.data.WinStreak++;
            PlayerData.data.Wins++;
        }
        else
        {
            hudScript.ActivatePanel("You Lost!");
            PlayerData.data.WinStreak = 0;
            PlayerData.data.Losses++;
        }
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
            SetColors();
            CheckForAttack();
            StartGame();
        }
    }

    public void RemoveGame()
    {
        if (gameSession.players.Count == 1)
        {
            SaveManager.Instance.RemoveGame("games/" + PlayerData.data.activeGameID);
            Debug.Log("RemoveGame");
        }
        else
        {
            if (gameSession.Player1ID == FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            {
                gameSession.players.RemoveAt(0);
            }
            else
            {
                gameSession.players.RemoveAt(1);
            }
            SaveGame();
            PlayerData.data.activeGameID = "";
            SaveManager.Instance.SaveData(PlayerData.userPath, JsonUtility.ToJson(PlayerData.data));
        }
    }


}
