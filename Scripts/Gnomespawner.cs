using UnityEngine;
using System.Collections;

public class Gnomespawner : MonoBehaviour {

    public GameObject gnome;
    private float timer;
    private float reduction = 0.0f;
    private float minTime = 5.0f;
    private float passedTime = 0.0f;
	// Use this for initialization
	void Start () 
    {
        timer = 5 * Random.value;
        Invoke("Spawn", timer + minTime - reduction);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void Spawn()
    {
        GameObject go = (GameObject)Instantiate(gnome, transform.position, transform.rotation);
        passedTime += timer;
        if (passedTime > 60.0f)
        {
            if (reduction < minTime)
                reduction += 0.5f;
            passedTime = 0;
        }
        timer = 5 * Random.value;
        Invoke("Spawn", timer + minTime - reduction);
    }
}
