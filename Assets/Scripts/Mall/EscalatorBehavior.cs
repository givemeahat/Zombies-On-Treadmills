using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscalatorBehavior : MonoBehaviour
{
    public bool isReversed;

    // Start is called before the first frame update
    void Start()
    {
        if (isReversed)
        {
            this.GetComponent<Animator>().Play("Escalator_Reversed");
        }
    }
}
