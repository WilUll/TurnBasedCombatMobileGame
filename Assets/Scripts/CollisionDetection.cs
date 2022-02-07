using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Opponent"))
        {
            SaveManager.Instance.SaveOpponent(collision.gameObject);
            gameObject.GetComponent<FadeScreen>().Fade();
            SceneManager.LoadScene("BattleMode");

        }
    }
}
