using UnityEngine;
using System.Collections;

public class StoneCoverScript : MonoBehaviour {

    public GameObject coverFull;
    public GameObject coverTwoThirds;
    public GameObject coverOneThird;
    private int maxHP = 500;
    private int hp = 500;
    
    void Awake()
    {
        coverFull.SetActive(true);
    }
    public void Hit()
    {
        hp -= 1;
        if (hp == (Mathf.Round(maxHP*0.66f)))
        {
            coverFull.SetActive(false);
            coverTwoThirds.SetActive(true);
        }
        else if (hp == (Mathf.Round(maxHP *0.33f)))
        {
            coverFull.SetActive(false);
            coverTwoThirds.SetActive(false);
            coverOneThird.SetActive(true);
        }
        else if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
