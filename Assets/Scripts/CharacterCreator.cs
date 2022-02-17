using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Auth;

public class CharacterCreator : MonoBehaviour
{
    public Image colorImage;
    public Slider colorSlider;
    public TMP_InputField inputField;

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

        SavePlayer();
    }


    void SavePlayer()
    {
        PlayerData.data.ColorHUE = colorSlider.value;
        PlayerData.data.Name = inputField.text;

        PlayerData.SaveData();
    }
}