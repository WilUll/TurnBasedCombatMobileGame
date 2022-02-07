using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
}