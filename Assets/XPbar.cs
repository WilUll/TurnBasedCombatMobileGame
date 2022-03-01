using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class XPbar : MonoBehaviour
{
    public TMP_Text xpText;
    public TMP_Text levelText;
    Slider xpSlider;
    // Start is called before the first frame update
    void Start()
    {
        xpSlider = GetComponent<Slider>();

        xpSlider.maxValue = (PlayerData.data.Level + 1) * 100;
        xpSlider.value = PlayerData.data.Exp;

        xpText.text = xpSlider.value + "/" + xpSlider.maxValue;
        levelText.text = PlayerData.data.Level.ToString();
    }
}
