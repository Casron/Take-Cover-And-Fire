using UnityEngine;
using System.Collections;

public class BombSpawner : MonoBehaviour {

    private float reduction;
    public GameObject bomb;
    private GameObject playerFollow;
	// Use this for initialization
	void Start () 
    {
        Invoke("DelayEnd", 90.0f);
        reduction = 0;
        playerFollow = GameObject.FindGameObjectWithTag("player");
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    void DelayEnd()
    {
        Invoke("BombCreate", 5.0f * Random.value);
    }

    void BombCreate()
    {
        transform.position = new Vector3(playerFollow.transform.position.x, 7.0f, playerFollow.transform.position.z - 2.0f + (4.0f * Random.value));
        Instantiate(bomb, transform.position, transform.rotation);
        if (reduction < 5.0f)
            reduction += 0.01f;
        Invoke("BombCreate", 5.0f + (5.0f * Random.value) - reduction);
    }

}
