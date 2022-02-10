using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    GameObject playerObject;
    public GameObject enemyObject;
    int EnemyTotal = 5;
    GameObject[] enemyArray;
    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < EnemyTotal; i++)
        {
            enemyArray[i] = Instantiate(enemyObject, new Vector3(i, 0, 0), Quaternion.identity);
        }
    }
}
