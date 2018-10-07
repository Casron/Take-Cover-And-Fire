using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkirmishScoreKeepScript : MonoBehaviour {
    
    public Text tex;
    private int p1score;
    private int p2score;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject p1;
    public GameObject p2;
    public GameObject p1wins;
    public GameObject p2wins;
    private bool roundInProgress;
    // Use this for initialization
    void Start()
    {
        roundInProgress = true;
        GameObject[] g = GameObject.FindGameObjectsWithTag("player");
        for(int i = 0; i < g.Length; i++)
        {
            if (g[i].GetComponent<PlayerController>() != null)
            {
                p1 = g[i];
            }
            else if (g[i].GetComponent<AIscript>() != null)
            {
                p2 = g[i];
            }
        }
        p1score = 0;
        p2score = 0;
        tex.text = ("Player 1: " + p1score + "                    " + "Player 2: " + p2score);
    }

    // Update is called once per frame
    void Update()
    {
        if (roundInProgress)
        {
            if(p1.GetComponent<PlayerController>().getHP() <= 0)
            {
                UpdateAiScore();
                roundInProgress = false;
                if (p2score < 5)
                {
                    Invoke("NextRound", 5.0f);
                }
                else
                {
                    Invoke("EndGame", 7.0f);
                    p2wins.SetActive(true);
                }
            }
            else if (p2.GetComponent<AIscript>().getHP() <= 0)
            {
                UpdateScore();
                roundInProgress = false;
                if(p1score < 5)
                    Invoke("NextRound", 5.0f);
                else
                {
                    p1wins.SetActive(true);
                    Invoke("EndGame", 7.0f);
                }
            }
        }
    }
    public void UpdateScore()
    {
        p1score += 1;
        tex.text = ("Player 1: " + p1score + "                    " + "Player 2: " + p2score);
    }
    public void UpdateAiScore()
    {
        p2score += 1;
        tex.text = ("Player 1: " + p1score + "                    " + "Player 2: " + p2score);
    }
    private void NextRound()
    {
        p1.transform.position = spawn1.transform.position;
        p2.transform.position = spawn2.transform.position;
        p1.GetComponent<PlayerController>().setHP();
        p2.GetComponent<AIscript>().setHP();
        roundInProgress = true;
    }
    private void EndGame()
    {
        Application.LoadLevel("MenuRoom");
    }
}
