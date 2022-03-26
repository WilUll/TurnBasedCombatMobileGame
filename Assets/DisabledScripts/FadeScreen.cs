using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreen : MonoBehaviour
{
    public Image fade;
    private void Start()
    {
        fade.CrossFadeAlpha(0f, 0f, false);
    }

    public void Fade()
    {
        fade.CrossFadeAlpha(1f, 0f, false);

    }
}
