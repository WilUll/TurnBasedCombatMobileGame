using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public Dictionary<int, int> attackRules;
    // Start is called before the first frame update
    void Start()
    {
        attackRules = new Dictionary<int, int>();

        attackRules.Add(1, 2);
        attackRules.Add(2, 3);
        attackRules.Add(3, 1);
    }

    //1. Close
    //2. Range
    //3. Counter
    //Close wins against range
    //Range wins against counter
    //Counter wins against Close

}
