using UnityEngine;
using System.Collections;

public class AirStrikeItemScript : MonoBehaviour {

    public GameObject AirStrike;
	// Use this for initialization
	void Start () 
    {
        GameObject[] s = GameObject.FindGameObjectsWithTag("AirStrikeItemSpawner");
        for(int i = 0; i < s.Length; i++)
        {
            s[i].GetComponent<AirStrikeItemSpawn>().Reset();
        }
        InvokeRepeating("Rotate", 0.01f, 0.01f);
        Invoke("RIP", 15.0f);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.GetComponent<PlayerController>() != false)
        {
            GameObject a = GameObject.FindGameObjectWithTag("AirStrikeSpawn");
            Instantiate(AirStrike, a.transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    void Rotate()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + 0.01f, transform.localEulerAngles.z);
    }
    void RIP()
    {
        Destroy(gameObject);
    }
}
