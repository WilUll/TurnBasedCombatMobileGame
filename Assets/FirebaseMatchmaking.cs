using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirebaseMatchmaking : MonoBehaviour
{
    private void Start()
    {

    }
    public void LookForAGame()
    {
        SaveManager.Instance.LoadData("games/", OnGamesLoaded);
    }

    public void OnGamesLoaded(List<string> gameList)
    {
        foreach (var game in gameList)
        {
            GameInfo gameInfo = JsonUtility.FromJson<GameInfo>(game);
            if (!gameInfo.isFull)
            {
                JoinGame(gameInfo);
                return;
            }
        }
        CreateGameSession();
    }

    private void JoinGame(GameInfo gameToJoin)
    {
        PlayerData.data.activeGameID = gameToJoin.gameID;
        gameToJoin.players.Add(PlayerData.data);
        gameToJoin.isFull = true;

        string gameDataJson = JsonUtility.ToJson(gameToJoin);

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

        string gameData = JsonUtility.ToJson(gameSession);

        string path = "games/" + key;
        SaveManager.Instance.SaveData(path, gameData);
        SceneManager.LoadScene("OnlineTest");
    }
}
