using Firebase.Auth;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseMatchmaking : MonoBehaviour
{
    public void LookForAGame()
    {
        SaveManager.Instance.LoadData("games/", OnGamesLoaded);
    }

    public void OnGamesLoaded(List<string> gameList)
    {
        bool createAGame = true;
        foreach (var game in gameList)
        {
            GameInfo gameInfo = JsonUtility.FromJson<GameInfo>(game);
            if (!gameInfo.isFull)
            {
                createAGame = false;
                JoinGame(gameInfo);
                return;
            }
        }
        if (createAGame)
        {
            CreateGameSession();
        }
    }

    private void JoinGame(GameInfo gameToJoin)
    {
        PlayerData.data.activeGameID = gameToJoin.gameID;
        gameToJoin.players.Add(PlayerData.data);
        gameToJoin.isFull = true;
        gameToJoin.Player2ID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        gameToJoin.userIDTurn = gameToJoin.Player1ID;

        string gameDataJson = JsonUtility.ToJson(gameToJoin);
        PlayerData.SaveData();
        SaveManager.Instance.SaveData("games/" + gameToJoin.gameID, gameDataJson);
        SceneManager.LoadScene("OnlineTest");
    }

    public void CreateGameSession()
    {
        GameInfo gameSession = new GameInfo();


        string key = SaveManager.Instance.GetKey("games/");
        gameSession.gameID = key;

        gameSession.players = new List<PlayerSaveData>();
        PlayerData.data.activeGameID = key;

        gameSession.players.Add(PlayerData.data);
        gameSession.Player1ID = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        string gameData = JsonUtility.ToJson(gameSession);

        string path = "games/" + key;
        PlayerData.SaveData();
        SaveManager.Instance.SaveData(path, gameData);
        SceneManager.LoadScene("OnlineTest");
    }
}
