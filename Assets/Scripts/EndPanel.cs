using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class EndPanel : MonoBehaviour
{
    public GameObject GameCanvas;
    public Slider ExpBar;
    public TextMeshProUGUI ExpText;

    public PlayerInfo playerScript;

    bool startCount = false;
    int expAdded;
    string userPath;
    public int xpToAdd;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Setup());
        userPath = "users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId;

    }

    IEnumerator Setup()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log(playerScript.level);
        ExpBar.maxValue = (playerScript.level + 1) * 100;
        ExpBar.value = playerScript.Exp;
        //TODO: SLIDER MAX = Player level + 1 * 100;
        ExpText.text = ExpBar.value + " / " + ExpBar.maxValue;
        if (playerScript.WinStreak > 0)
        {
            playerScript.AddXP(50 * playerScript.WinStreak);
        }
        else
        {
            playerScript.AddXP(25);
        }
    }

    private void Update()
    {
        if (startCount)
        {
            if (expAdded >= 50)
            {
                startCount = false;
                return;
            }
            ExpBar.value++;
            ExpText.text = ExpBar.value + " / " + ExpBar.maxValue;
            expAdded++;
        }
    }

    public void StartCounting()
    {
        startCount = true;
    }

    public void LoadPrevScene()
    {
        SaveManager.Instance.LoadData("users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId, OnLoadData);
        SceneManager.LoadScene("GameView");
    }


    void OnLoadData(string json)
    {
        PlayerSaveData saveData;

        if (json != null)
        {
            saveData = JsonUtility.FromJson<PlayerSaveData>(json);
        }
        else
        {
            return;
        }

        saveData.Level = playerScript.level;
        saveData.Exp = playerScript.Exp;
        saveData.WinStreak = playerScript.WinStreak;

        SaveData(saveData);
    }

    private void SaveData(PlayerSaveData data)
    {
        SaveManager.Instance.SaveData(userPath, JsonUtility.ToJson(data));
    }
}
