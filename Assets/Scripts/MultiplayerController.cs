using UnityEngine;
using System.Collections;
using Parse;
using System.Collections.Generic;

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

	public static string mode;

	ParseController.ListMapOperation listMaps;
	
	public List<ParseController.MapEntity> listLevelsSPM = new List<ParseController.MapEntity>();

	// Use this for initialization
	void Start () {
		hideAllMapButtons ();

		//mode = "singleplayer";

		Debug.Log ("mode list maps " + mode);


		if (mode=="multiplayer") StartCoroutine("LoadAllMaps");
		else if (mode == "singleplayer") initMapsSPM();
		else if (mode=="mapeditor") initMapsMapEditor();

	}
	

	IEnumerator LoadAllMaps() {
		Debug.Log("LoadAllMaps");
		
		listMaps = new ParseController.ListMapOperation();
		listMaps.run();
		while (!listMaps.IsCompleted) yield return null;

		countNumMaps ();
		loadMapButtons ();
	}

	void initMapsSPM () {

		listLevelsSPM.Add(new ParseController.MapEntity("MAP_NAME_HERE",new int[,]{{11,3,0},{12,3,0},{11,4,0},{12,4,0},{13,4,0},{10,5,0},{13,5,0},{9,6,0},{14,6,0},{9,7,0},{14,7,0},{9,8,0},{10,8,0},{11,8,0},{12,8,0},{13,8,0},{10,9,0}}));


		listLevelsSPM.Add(new ParseController.MapEntity("hahaha",new int[,]
		                                   {{1,2,1},{1,6,2}}
		));
		
		listLevelsSPM.Add(new ParseController.MapEntity("zzzzz",new int[,]
		                                         {{1,2,1},{1,2,2}}
		));
		
		listLevelsSPM.Add(new ParseController.MapEntity("xxxxx",new int[,]
		                                         {{1,2,1},{1,2,2}}
		));


		countNumMaps ();
		loadMapButtons ();
	}

	void initMapsMapEditor() {
	}

	void countNumMaps() {
		numMaps = 0;
		if (mode == "multiplayer") {
				foreach (ParseController.MapEntity map in listMaps.result) {
						numMaps ++;
				}
		} else if (mode == "singleplayer") {
			numMaps = listLevelsSPM.Count;
		}
		else if (mode=="mapeditor") {
		}

		numMaxPages = Mathf.CeilToInt(numMaps/numButtonsPerPage);
		Debug.Log ("num pages" + numMaxPages);
		Debug.Log ("num maps" + numMaps);
	}

	void loadMapButtons() {
		int numCurrentMap = 0;
		int numButton = 0;
		paintNextPrevButtons ();

		UnityEngine.UI.Text textLev1 = map1Button.GetComponentInChildren<UnityEngine.UI.Text> ();
		UnityEngine.UI.Text textLev2 = map2Button.GetComponentInChildren<UnityEngine.UI.Text> ();
		UnityEngine.UI.Text textLev3 = map3Button.GetComponentInChildren<UnityEngine.UI.Text> ();

		List<ParseController.MapEntity> theList = new List<ParseController.MapEntity>();

		if (mode == "multiplayer") {
			theList = listMaps.result;
		} else if (mode == "singleplayer") {
			theList = listLevelsSPM;
		}
		else if (mode=="mapeditor") {
			theList=null;
		}

		foreach (ParseController.MapEntity map in theList)
		{

			if (numCurrentMap>=(numPage*numButtonsPerPage+numButtonsPerPage)) break;
			else if (numCurrentMap<(numPage*numButtonsPerPage)) {
			} else {
				numButton = numCurrentMap%numButtonsPerPage;

				if(numButton==0) { 
					map1=map;
					textLev1.text = "Map "+(numCurrentMap+1)+" "+map.id;
				} else if (numButton==1) { 
					map2=map;
					textLev2.text = "Map "+(numCurrentMap+1)+" "+map.id;
				} else if (numButton==2) { 
					map3=map;
					textLev3.text = "Map "+(numCurrentMap+1)+" "+map.id;
				}
			}
		
			numCurrentMap++;
		}

		showAllMapButtons();
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
		if (numPage == numMaxPages-1) {
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
