using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    //public GameManager gm;
    public Transform waypoint;

    // Start is called before the first frame update
    public void Flash()
    {
        GetComponent<Animator>().SetTrigger("Flash");
    }
}
