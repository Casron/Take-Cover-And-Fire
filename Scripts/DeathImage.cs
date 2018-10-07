using UnityEngine;
using System.Collections;

public class DeathImage : MonoBehaviour {

    public RectTransform rect;
	// Use this for initialization
	void Start () 
    {
        rect.position = new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0);
        rect.sizeDelta = new Vector2(Screen.width, Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
