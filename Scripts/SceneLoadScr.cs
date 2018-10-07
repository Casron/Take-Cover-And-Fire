using UnityEngine;
using System.Collections;

public class SceneLoadScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void loadArcade()
    {
        Application.LoadLevel("ArcadeScene");
    }
    public void loadMenu()
    {
        Application.LoadLevel("MenuRoom");
    }
    public void loadSkirmish()
    {
        Application.LoadLevel("SkirmishRoom");
    }
    public void loadCredits()
    {
        Application.LoadLevel("CreditsRoom");
    }
    public void loadTutorial()
    {
        Application.LoadLevel("TutorialRoom");
    }
    public void loadMultiplay()
    {
        Application.LoadLevel("MultiplayerLobby");
    }
    public void end()
    {
        Application.Quit();
    }
}
