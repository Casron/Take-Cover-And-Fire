using UnityEngine;
using System.Collections;

public class MultiPlayerCameraFollow : MonoBehaviour {

    private GameObject player;
	// Use this for initialization
	void Start () {
	
	}

    public void SetPlayerFollow(GameObject g)
    {
        player = g;
    }
	
	// Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(6.5f, player.transform.position.y + 1.0f, (player.transform.position.z - 6.0f) + (6.0f * (Input.mousePosition.x / (Screen.width / 2))));
    }
}
