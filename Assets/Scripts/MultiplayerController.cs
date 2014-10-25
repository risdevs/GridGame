using UnityEngine;
using System.Collections;
using Parse;

public class MultiplayerController : MonoBehaviour {

	public UnityEngine.UI.Button map1Button;
	public UnityEngine.UI.Button map2Button;
	public UnityEngine.UI.Button map3Button;
	
	public UnityEngine.UI.Button nextButton;
	public UnityEngine.UI.Button prevButton;
	
	public UnityEngine.UI.Button backButton;

	int numPage = 0;
	int numMaxPages = 0;
	int numMaps = 0;

	ParseController.MapEntity map1;
	ParseController.MapEntity map2;
	ParseController.MapEntity map3;

	int numButtonsPerPage=3;

	ParseController.ListMapOperation listMaps;

	// Use this for initialization
	void Start () {
		/*var query = ParseObject.GetQuery("TestObject");
		query.FindAsync().ContinueWith(t => {
			foreach (ParseObject obj in t.Result) {
				Debug.Log(obj.ObjectId);
			}
		});*/

		hideAllMapButtons ();
		StartCoroutine("LoadAllMaps");

	}
	

	IEnumerator LoadAllMaps() {
		Debug.Log("LoadAllMaps");
		
		listMaps = new ParseController.ListMapOperation();
		listMaps.run();
		while (!listMaps.IsCompleted) yield return null;

		loadAllMaps ();
		loadMapButtons ();
	}


	void loadAllMaps() {
		foreach (ParseController.MapEntity map in listMaps.result) {
			numMaps ++;
		}
		numMaxPages = Mathf.CeilToInt(numMaps/numButtonsPerPage);
	}

	void loadMapButtons() {
		int numCurrentMap = 0;
		int numButton = 0;
		paintNextPrevButtons ();

		UnityEngine.UI.Text textLev1 = map1Button.GetComponentInChildren<UnityEngine.UI.Text> ();
		UnityEngine.UI.Text textLev2 = map2Button.GetComponentInChildren<UnityEngine.UI.Text> ();
		UnityEngine.UI.Text textLev3 = map3Button.GetComponentInChildren<UnityEngine.UI.Text> ();

		foreach (ParseController.MapEntity map in listMaps.result)
		{

			if (numCurrentMap>=(numPage*numButtonsPerPage+numButtonsPerPage)) break;
			else if (numCurrentMap<(numPage*numButtonsPerPage)) {
			} else {
				numButton = numCurrentMap%numButtonsPerPage;

				if(numButton==0) { 
					map1=map;
					textLev1.text = "Map "+(numCurrentMap+1)+" "+map.parseObject.ObjectId;
				} else if (numButton==1) { 
					map2=map;
					textLev2.text = "Map "+(numCurrentMap+1)+" "+map.parseObject.ObjectId;
				} else if (numButton==2) { 
					map3=map;
					textLev3.text = "Map "+(numCurrentMap+1)+" "+map.parseObject.ObjectId;
				}
			}
		
			numCurrentMap++;
		}

		showAllMapButtons ();
		if (numButton == 0) {
			map2Button.enabled = false;
			textLev2.text = "";
		} else if (numButton == 1) {
			map3Button.enabled = false;
			textLev3.text = "";
		}
	}

	void paintNextPrevButtons() {
		prevButton.enabled = true;
		nextButton.enabled = true;
		if (numPage == 0) {
			prevButton.enabled=false;
		}
		if (numPage == numMaxPages) {
			nextButton.enabled=false;
		}
	}

	void showAllMapButtons(){
		map1Button.enabled = true;
		map2Button.enabled = true;
		map3Button.enabled = true;
	}

	void hideAllMapButtons(){
		map1Button.enabled = false;
		map2Button.enabled = false;
		map3Button.enabled = false;
	}

	public void nextPageButtons() {
		showAllMapButtons ();
		numPage++;
		loadMapButtons ();
	}

	public void prevPageButtons() {
		showAllMapButtons ();
		numPage--;
		loadMapButtons ();
	}

	public void playMap(int i) {
		if (i==1) GameController.mapToLoad = map1;
		else if (i==2) GameController.mapToLoad = map2;
		else if (i==3) GameController.mapToLoad = map3;
		Application.LoadLevel("Game");
	}

	public void goMainMenuButton() {
		Application.LoadLevel("Main");
	}

	// Update is called once per frame
	void Update () {
	
	}

}
