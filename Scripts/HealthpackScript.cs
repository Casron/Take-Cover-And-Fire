using UnityEngine;
using System.Collections;

public class HealthpackScript : MonoBehaviour {

    public GameObject respawner;
	// Use this for initialization
	void Start () 
    {
        InvokeRepeating("Rotate", 0.001f, 0.001f);
	}
	
	// Update is called once per frame
	void Update () 
    {
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            PlayerController p = other.gameObject.GetComponent<PlayerController>();
            if (p.getHP() < p.getMaxHP())
            {
                other.gameObject.GetComponent<PlayerController>().GiveHealth(50);
                Instantiate(respawner, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
    void Rotate()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 0.1f, transform.localEulerAngles.z);
    }
}
