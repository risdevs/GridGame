using UnityEngine;
using System.Collections;

public class GenericMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenScene(string scene) {
		Application.LoadLevel(scene);
	}
}
