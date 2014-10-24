using UnityEngine;
using System.Collections;

public class SinglePlayerMenu : MonoBehaviour {


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClickBack() {
        Application.LoadLevel("Main");
    }

    public void ClickLevel(int level) {
		GameController.LevelToLoad = level;
		Application.LoadLevel("Game");
    }
}
