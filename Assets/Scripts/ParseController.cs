using UnityEngine;
using System.Collections;
using Parse;

public class ParseController : ParseInitializeBehaviour{


	public override void Awake() {
		base.applicationID = "2QWerPx74sTKazgf92SYJuaMMP7jpOy0lB6fJ3NW";
		base.dotnetKey = "JNKPfQqY2vhgk6CnDJsSgTorwgEeG4rQVaXbYbhd";
		base.Awake();
	}

	public bool IsLoggedIn() {
		return ParseUser.CurrentUser != null;
	}

	public void Login(string name) {
		ParseUser.LogInAsync (name, "socialpoint").ContinueWith (t => {
						if (t.IsFaulted || t.IsCanceled) {
								// The login failed. Check the error to see why.
						} else {
								// Login was successful.
						}
				});
	}

	public void Register(string name) {
	}
}
