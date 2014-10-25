using UnityEngine;
using System.Collections;
using Parse;

public class MultiplayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var query = ParseObject.GetQuery("TestObject");
		query.FindAsync().ContinueWith(t => {
			foreach (ParseObject obj in t.Result) {
				Debug.Log(obj.ObjectId);
			}
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
