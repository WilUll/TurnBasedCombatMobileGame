using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentSummon : MonoBehaviour
{
    PlayerInfo playerScript;
    OpponentInfo opponentInfoScript;
    // Start is called before the first frame update
    void Start()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInfo>();
        opponentInfoScript = gameObject.GetComponent<OpponentInfo>();


    }
}
