using UnityEngine;
using System.Collections;
using Parse;

public class GenericMenuScript : MonoBehaviour {

    public UnityEngine.UI.InputField NameInput;

    // Use this for initialization
	void Start () {
        if (NameInput != null)
        {
            NameInput.text.text = ParseUser.CurrentUser.Username;
        }	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OpenScene(string scene) {
		Application.LoadLevel(scene);
	}

	public void loadListOfMaps(string mode) {
		MultiplayerController.mode = mode;
		Application.LoadLevel ("MultiPlayerMenu");
	}

    public void UpdateUserName() {
        //ParseController.RenameUser(NameInput.text.text);
    }

    public void OnDestroy() {
        if (NameInput != null) 
            ParseController.RenameUser(NameInput.text.text);
    }
}
