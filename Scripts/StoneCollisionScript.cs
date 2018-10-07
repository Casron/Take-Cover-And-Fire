using UnityEngine;
using System.Collections;

public class StoneCollisionScript : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, -transform.localEulerAngles.y, transform.localEulerAngles.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
