using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    public Text tex;
    private int score;
	// Use this for initialization
	void Start () 
    {
        score = 0;
        UpdateScore(0);
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
    public void UpdateScore(int x)
    {
        score += x;
        tex.text = ("Score " + score);
    }
    public int getScore()
    {
        return score;
    }
}
