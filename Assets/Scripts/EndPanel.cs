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
    public TMP_Text levelText;
    public TMP_Text xpAddText;

    public PlayerInfo playerScript;

    bool startCount = false;
    int expAdded;
    public int xpToAdd;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        ExpBar.value = PlayerData.data.Exp;
        yield return new WaitForSeconds(1f);
        levelText.text = PlayerData.data.Level.ToString();
        ExpBar.maxValue = (PlayerData.data.Level + 1) * 100;
        //TODO: SLIDER MAX = Player level + 1 * 100;
        ExpText.text = ExpBar.value + " / " + ExpBar.maxValue;
        if (PlayerData.data.WinStreak > 0)
        {
            xpToAdd = 50 * PlayerData.data.WinStreak;
            xpAddText.text = "+ " + xpToAdd + " XP";
            PlayerData.AddXP(xpToAdd);
        }
        else
        {
            xpToAdd = 25;
            xpAddText.text = "+ " + xpToAdd + " XP";
            PlayerData.AddXP(xpToAdd);
        }
        SaveManager.Instance.SaveData(PlayerData.userPath, JsonUtility.ToJson(PlayerData.data));
    }

    private void Update()
    {
        if (startCount)
        {
            if (expAdded >= xpToAdd)
            {
                startCount = false;
                return;
            }
            ExpBar.value++;
            ExpText.text = ExpBar.value + " / " + ExpBar.maxValue;
            expAdded++;

            if (ExpBar.value == ExpBar.maxValue)
            {
                ExpBar.value = 0;
                levelText.text = PlayerData.data.Level.ToString();
                ExpBar.maxValue = (PlayerData.data.Level + 1) * 100;
            }
        }
    }

    public void StartCounting()
    {
        startCount = true;
    }
}
