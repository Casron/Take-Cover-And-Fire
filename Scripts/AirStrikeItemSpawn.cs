using UnityEngine;
using System.Collections;

public class AirStrikeItemSpawn : MonoBehaviour {

	// Use this for initialization
    public GameObject item;
    private float minTime = 120.0f;
    // Use this for initialization
    void Start()
    {
        Invoke("Respawn", minTime + (60.0f * Random.value));
    }

    // Update is called once per frame
    void Respawn()
    {
        Instantiate(item, transform.position, transform.rotation);
        Destroy(gameObject);
    }
    public void Reset()
    {
        CancelInvoke("Respawn");
        Invoke("Respawn", minTime + (60.0f * Random.value));
    }
}
