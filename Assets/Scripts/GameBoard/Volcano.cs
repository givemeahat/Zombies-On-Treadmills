using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : MonoBehaviour
{
    //public GameManager gm;
    public Transform waypoint;
    public GameObject smokeFX;
    public Material smokeMat;

    // Start is called before the first frame update
    void Start()
    {
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        smokeFX.GetComponent<Animator>().StopPlayback();
        smokeFX.GetComponent<Animator>().Play("Smoke", 0, Random.value);

        Renderer rend = smokeFX.GetComponent<Renderer>();

        rend.material = new Material(smokeMat);
    }

    public void Burn()
    {
        this.GetComponent<Animator>().SetTrigger("Burning");
    }
}
