using System;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class PlayerSaveData
{
    public string Name;
    public float ColorHUE;
    public float Exp;
    public int Level;

    //Not Active
    public int Wins;
    public int Losses;
    public int WinStreak;
}

[Serializable]
public class GameInfo
{
    public string displayName;
    public string gameID;
    public List<PlayerSaveData> players;
}