using UnityEngine;
using System.Collections;

public class StoneChipScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        Invoke("Dest", 5.0f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetDirection(float x, float y, float z)
    {
        Vector3 dir = new Vector3(x - .3f + (.6f * Random.value), y - .3f + (.6f * Random.value), z - .3f + (.6f * Random.value));
        GetComponent<Rigidbody>().AddForce(dir * .0012f);
    }
    void Dest()
    {
        Destroy(gameObject);
    }
}
