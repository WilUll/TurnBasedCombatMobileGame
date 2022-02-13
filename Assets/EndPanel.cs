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

    public PlayerInfo playerScript;

    bool startCount = false;
    int expAdded;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Setup());

    }

    IEnumerator Setup()
    {
        yield return new WaitForSeconds(0.2f);
        Debug.Log(playerScript.level);
        ExpBar.maxValue = (playerScript.level + 1) * 100;
        ExpBar.value = playerScript.Exp;
        //TODO: SLIDER MAX = Player level + 1 * 100;
        ExpText.text = ExpBar.value + " / " + ExpBar.maxValue;
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
            ExpBar.value ++;
            ExpText.text = ExpBar.value + " / " + ExpBar.maxValue;
            expAdded++;
        }
    }

    public void StartCounting()
    {
        startCount = true;
    }
}
