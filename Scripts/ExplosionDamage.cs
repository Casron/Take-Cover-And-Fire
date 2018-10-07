using UnityEngine;
using System.Collections;

public class ExplosionDamage : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Invoke("RIP", 0.5f);
        GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.GetComponent<PlayerController>() != null)
        {
            other.transform.gameObject.GetComponent<PlayerController>().DoBombDamage(50);
        }
    }
    void RIP()
    {
        Destroy(gameObject);
    }
}
