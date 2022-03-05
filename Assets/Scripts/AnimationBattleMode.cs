using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBattleMode : MonoBehaviour
{
    public void TakeDamageAnimation()
    {
        if (gameObject.transform.parent.CompareTag("Player"))
        {
            Animator anim = GameObject.FindGameObjectWithTag("Opponent").GetComponent<Animator>();

            anim.SetTrigger("TakeDamage");
            ServiceLocator.GetAudioProvider().PlayOneShot("damage");

        }
        else
        {
            Animator anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

            anim.SetTrigger("TakeDamage");
            ServiceLocator.GetAudioProvider().PlayOneShot("damage");
        }

    }
}
