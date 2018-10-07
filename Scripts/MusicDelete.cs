using UnityEngine;
using System.Collections;

public class MusicDelete : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        GameObject g = GameObject.FindGameObjectWithTag("MusicPlayer");
        Destroy(g);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
