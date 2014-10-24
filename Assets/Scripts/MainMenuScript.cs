using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

   
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClickSinglePlayer() {
        Application.LoadLevel("SinglePlayerMenu");
    }

    public void ClickMultiPlayer() {
        Application.LoadLevel("MultiPlayerMenu");
    }
}
