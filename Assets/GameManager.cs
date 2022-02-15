using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    GameInfo game;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {
        SaveManager.Instance.LoadData("games/" + PlayerData.data.activeGameID, OnDataLoaded);
    }

    private void OnDataLoaded(string gameInfo)
    {
        game = JsonUtility.FromJson<GameInfo>(gameInfo);

        gameObject.GetComponent<FirebaseOnline>().Subscribe(game.gameID);
    }

    public void ChangeData(int value)
    {
        game.userIDTurn = value.ToString();
        SaveManager.Instance.SaveData("games/" + game.gameID, JsonUtility.ToJson(game));

    }


    public void UpdateGame()
    {
        Debug.Log("Updated");

        SaveManager.Instance.LoadData("games/" +game.gameID, RefreshGame);
    }

    private void RefreshGame(string newGame)
    {
        game = JsonUtility.FromJson<GameInfo>(newGame);

        text.text = game.userIDTurn.ToString();
    }
}
