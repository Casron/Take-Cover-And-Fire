using UnityEngine;
using System.Collections;

public class MuzzleFlashScript : MonoBehaviour {

    private Rigidbody rB;
	// Use this for initialization
    void Awake()
    {
        rB = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Invoke("Death", 0.2f);
    }
    void Death()
    {
        Destroy(gameObject);
    }

    public void setDir(float i)
    {
        if (i < 0)
        {
            transform.localEulerAngles = new Vector3(0.0f, 180f, 0.0f);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0.0f,0f,0.0f);
        }
    }
    public void setVel(float yV, float zV)
    {
        rB.velocity = new Vector3(0, yV, zV);
    }
}
