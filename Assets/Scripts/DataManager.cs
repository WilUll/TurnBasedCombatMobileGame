using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerSaveData
{
    public string Name;
    public float ColorHUE;
    public float Exp;
    public int Level;
    public int WinStreak;
    public string activeGameID;

    //Not Active
    public int Wins;
    public int Losses;
}

[Serializable]
public class GameInfo
{
    public string gameID;
    public bool isFull;
    public string userIDTurn;
    public List<PlayerSaveData> players;
    public string Player1ID;
    public string Player2ID;
    public int Player1Attack;
    public int Player2Attack;
}