using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;

public class CharacterCreator : MonoBehaviour
{
    public Image colorImage;
    public Slider colorSlider;
    public TMP_InputField inputField;


    void Start()
    {
        colorImage.color = Color.HSVToRGB(colorSlider.value, 0.85f, 0.85f);
    }

    public void ChangeCharacterColor()
    {
        colorImage.color = Color.HSVToRGB(colorSlider.value, 0.85f, 0.85f);
    }

    public void SaveData()
    {
        PlayerSaveData saveData = new PlayerSaveData();
        saveData.Name = inputField.text;
        saveData.ColorHUE = colorSlider.value;

        //Convert saveData object to JSON
        string jsonString = JsonUtility.ToJson(saveData);

        string userPath = "users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        SaveManager.Instance.SaveData(userPath, JsonUtility.ToJson(jsonString));
    }
}