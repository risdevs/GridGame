using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;


public class RankingController : MonoBehaviour {

	public UnityEngine.UI.InputField NameInput;

	// Use this for initialization
	void Start () {
		ParseUser user = ParseUser.CurrentUser;
		if (user == null) {
			user = new ParseUser ()
			{
				Username = "" + Random.Range(1, 100000),
				Password = "" + "" + Random.Range(1, 100000)
			};
			user["title"] = "Player " + user.Username;

			// other fields can be set just like with ParseObject
			Task signUpTask = user.SignUpAsync ();			
		} 
		if (!user.ContainsKey ("title")) {
			user["title"] = "Player " + user.Username;
		}

		NameInput.text.text = user.Get<string>("title");
	}

	public void UpdateName() {
		ParseUser user = ParseUser.CurrentUser;
		user ["title"] = NameInput.text.text;
		user.SaveAsync ();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
