using System;
using UnityEngine;

[Serializable]
public class PlayerSaveData
{
    public string Name;
    public float ColorHUE;
    public float Exp;
}

public class OpponentSaveData
{
    public string Name;
    public float ColorHUE;
    public int level;
}


public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    public CharacterCreator charInfo;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        PlayerSaveData saveData = new PlayerSaveData();


        saveData.Name = charInfo.inputField.text;
        saveData.ColorHUE = charInfo.colorSlider.value;

        string jsonString = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString("PlayerSaveData", jsonString);

        Debug.Log(jsonString);
    }

    public void PlayerSave(GameObject player)
    {
        PlayerSaveData saveData = new PlayerSaveData();
        PlayerInfo playInfo = player.GetComponent<PlayerInfo>();

        saveData.Name = playInfo.Name;
        saveData.ColorHUE = playInfo.ColorHUE;
        saveData.Exp = playInfo.Exp;

        string jsonString = JsonUtility.ToJson(saveData);

        PlayerPrefs.SetString("PlayerSaveData", jsonString);
    }

    public void SaveOpponent(GameObject opponent)
    {
        OpponentSaveData opponentSaveData = new OpponentSaveData();
        OpponentInfo oppInfo = opponent.GetComponent<OpponentInfo>();

        opponentSaveData.Name = oppInfo.name;
        opponentSaveData.ColorHUE = oppInfo.ColorHUE;
        opponentSaveData.level = oppInfo.level;

        string jsonString = JsonUtility.ToJson(opponentSaveData);

        PlayerPrefs.SetString("OpponentSaveData", jsonString);

        Debug.Log(jsonString);
    }

    public void Load()
    {
        //Get the saved jsonString
        string jsonString = PlayerPrefs.GetString("PlayerSaveData");

        //Convert the data to a object

        try
        {
            PlayerSaveData loadedData = JsonUtility.FromJson<PlayerSaveData>(jsonString);
            Debug.Log("Loaded data");

        }
        catch (Exception)
        {
            Debug.Log("Data Failed To Load");
            throw;
        }
    }

    public void OpponentLoad()
    {
        //Get the saved jsonString
        string jsonString = PlayerPrefs.GetString("OpponentSaveData");

        //Convert the data to a object

        try
        {
            OpponentSaveData loadedData = JsonUtility.FromJson<OpponentSaveData>(jsonString);
            Debug.Log("Loaded data");

        }
        catch (Exception)
        {
            Debug.Log("Data Failed To Load");
            throw;
        }
    }
}
