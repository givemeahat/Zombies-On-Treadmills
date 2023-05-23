using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    //public GameManager gm;
    public Transform waypoint;
    public SpriteRenderer rend;

    public Sprite[] storeSprites;
    public bool isRandom = false;

    private void Awake()
    {
        if (isRandom)
        {
            int _spriteNum = Random.Range(0, storeSprites.Length);
            rend.sprite = storeSprites[_spriteNum];
        }
    }

    // Start is called before the first frame update
    public void Flash()
    {
        GetComponent<Animator>().SetTrigger("Flash");
    }
}
