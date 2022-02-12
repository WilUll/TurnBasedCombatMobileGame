using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class EndPanel : MonoBehaviour
{
    public GameObject GameCanvas;
    public Slider ExpBar;
    public TextMeshProUGUI ExpText;

    // Start is called before the first frame update
    void Start()
    {
        //TODO: SLIDER MAX = Player level + 1 * 100;
        ExpText.text = "0 / " + ExpBar.maxValue; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
