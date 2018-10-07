using UnityEngine;
using System.Collections;

public class MusicPlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        DontDestroyOnLoad(gameObject);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
