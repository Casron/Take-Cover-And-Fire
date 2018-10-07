using UnityEngine;
using System.Collections;

public class UnderFootScript : MonoBehaviour {

    public GameObject par;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    void OnTriggerEnter(Collider Other)
    {
        if (Other.gameObject.tag == "ground" || Other.gameObject.tag == "player" || Other.gameObject.tag == "barricade" || Other.gameObject.tag == "enemy")
        {
            if (par.GetComponent<PlayerController>() != null)
                par.GetComponent<PlayerController>().JumpReset();
            else if (par.GetComponent<AIscript>() != null)
                par.GetComponent<AIscript>().JumpReset();
            else if (par.GetComponent<GnomeScript>() != null)
                par.GetComponent<GnomeScript>().JumpReset();
            else if (par.GetComponent<ReptileScript>() != null)
                par.GetComponent<ReptileScript>().JumpReset();
            else if (par.GetComponent<MultiplayerPlayerController>() != null)
                par.GetComponent<MultiplayerPlayerController>().JumpReset();
        }
    }
    void OnTriggerStay(Collider Other)
    {
        if (Other.gameObject.tag == "ground" || Other.gameObject.tag == "player" || Other.gameObject.tag == "barricade" || Other.gameObject.tag == "enemy")
        {
            if (par.GetComponent<PlayerController>() != null)
                par.GetComponent<PlayerController>().JumpReset();
            else if (par.GetComponent<AIscript>() != null)
                par.GetComponent<AIscript>().JumpReset();
            else if (par.GetComponent<GnomeScript>() != null)
                par.GetComponent<GnomeScript>().JumpReset();
            else if (par.GetComponent<ReptileScript>() != null)
                par.GetComponent<ReptileScript>().JumpReset();
            else if (par.GetComponent<MultiplayerPlayerController>() != null)
                par.GetComponent<MultiplayerPlayerController>().JumpReset();
        }
    }
    void OnTriggerExit(Collider Other)
    {
        if (Other.gameObject.tag == "ground" || Other.gameObject.tag == "player" || Other.gameObject.tag == "barricade" || Other.gameObject.tag == "enemy")
        {
            if (par.GetComponent<PlayerController>() != null)
                par.GetComponent<PlayerController>().Knockup();
            else if (par.GetComponent<AIscript>() != null)
                par.GetComponent<AIscript>().Knockup();
            else if (par.GetComponent<GnomeScript>() != null)
                par.GetComponent<GnomeScript>().Knockup();
            else if (par.GetComponent<ReptileScript>() != null)
                par.GetComponent<ReptileScript>().Knockup();
            else if (par.GetComponent<MultiplayerPlayerController>() != null)
                par.GetComponent<MultiplayerPlayerController>().Knockup();
        }
    }
}
