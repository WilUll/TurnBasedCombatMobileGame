using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameInfo game;
    // Start is called before the first frame update
    void Start()
    {
        SaveManager.Instance.LoadData("games/" + PlayerData.data.activeGameID, OnDataLoaded);
    }

    private void OnDataLoaded(string gameInfo)
    {
        game = JsonUtility.FromJson<GameInfo>(gameInfo);
    }
}
