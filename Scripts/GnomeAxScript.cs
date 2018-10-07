using UnityEngine;
using System.Collections;

public class GnomeAxScript : MonoBehaviour {

    public GameObject bloodSpray;
	// Use this for initialization
	void Start () {
	
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "player")
        {
            if (other.transform.gameObject.GetComponent<PlayerController>() != null)
            {
                other.transform.gameObject.GetComponent<PlayerController>().DoDamage(15);
                Quaternion q = transform.rotation;
                GameObject go = (GameObject)Instantiate(bloodSpray, transform.position, q);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
