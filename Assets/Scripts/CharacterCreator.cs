using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;

public class CharacterCreator : MonoBehaviour
{
    public Image colorImage;
    public Slider colorSlider;
    public TMP_InputField inputField;

    PlayerSaveData data;
    string userPath;
    void Start()
    {
        inputField.characterLimit = 20;
        colorImage.color = Color.HSVToRGB(colorSlider.value, 0.85f, 0.85f);
    }

    public void ChangeCharacterColor()
    {
        colorImage.color = Color.HSVToRGB(colorSlider.value, 0.85f, 0.85f);
    }

    public void SaveButton()
    {
        if (inputField.text == "") return;

        userPath = "users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        SaveManager.Instance.LoadData(userPath, OnLoaded);
    }

    void OnLoaded(string json)
    {
        if (json != null)
        {
            data = JsonUtility.FromJson<PlayerSaveData>(json);
        }
        else
        {
            data = new PlayerSaveData();
        }
        SavePlayer(data);
    }

    void SavePlayer(PlayerSaveData dataToSave)
    {
        dataToSave.ColorHUE = colorSlider.value;
        dataToSave.Name = inputField.text;


        SaveManager.Instance.SaveData(userPath, JsonUtility.ToJson(dataToSave));
    }
}