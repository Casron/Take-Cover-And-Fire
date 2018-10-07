using UnityEngine;
using System.Collections;

public class HealthpackRespawnScript : MonoBehaviour 
{
    public GameObject pack;
    private float minTime = 60;
	// Use this for initialization
	void Start () 
    {
        Invoke("Respawn", minTime + (60.0f * Random.value));
	}
	
	// Update is called once per frame
    void Respawn()
    {
        Instantiate(pack, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
