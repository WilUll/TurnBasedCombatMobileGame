using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadNextScene : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameView");
    }
    
    public void LoadOnlineScene()
    {
        SceneManager.LoadScene("OnlineBattle");
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(0);
    }
}
