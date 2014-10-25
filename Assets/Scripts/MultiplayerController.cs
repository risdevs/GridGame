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

	ParseController.MapEntity map1;
	ParseController.MapEntity map2;
	ParseController.MapEntity map3;

    public UnityEngine.UI.Image flag1;
    public UnityEngine.UI.Image flag2;
    public UnityEngine.UI.Image flag3;

    public UnityEngine.UI.Text scoreText;

    int numButtonsPerPage=3;

	public static string mode;
    	
    public List<ParseController.MapEntity> mapList;

	// Use this for initialization
	void Start () {

        //ParseController.CompleteSinglePlayerLevel(0);

		//mode = "singleplayer";

		Debug.Log ("mode list maps " + mode);

        mapList = new List<ParseController.MapEntity>();
        RefreshButtons();
        if (mode == "multiplayer" || mode == "mapeditor") 
            StartCoroutine("LoadMaps");
        else if (mode == "singleplayer")
        {
            LoadSinglePlayerMaps();
            scoreText.text = "SCORE: " + ParseController.GetScore();
        }


	}
	

	IEnumerator LoadMaps() {
		Debug.Log("LoadAllMaps");
		
		var listMaps = new ParseController.ListMapOperation();
        listMaps.run(mode=="mapeditor");
		while (!listMaps.IsCompleted) yield return null;
        Debug.Log("load complete");
        mapList = listMaps.result;
		RefreshButtons ();
	}


    void LoadSinglePlayerMaps() {
        mapList = new List<ParseController.MapEntity>();
        mapList.Add(new ParseController.MapEntity(1,new int[,]
		                                          {{0,0,4},{1,0,4},{2,0,4},{3,0,4},{4,0,4},{5,0,4},{6,0,4},{7,0,4},{8,0,4},{9,0,4},{10,0,4},{11,0,4},{12,0,4},{13,0,4},{14,0,4},{15,0,4},{16,0,4},{17,0,4},{18,0,4},{19,0,4},{20,0,4},{19,1,5},{5,5,2},{10,5,2},{15,5,2}}
        ));

        mapList.Add(new ParseController.MapEntity(2,new int[,]
		                                          {{0,0,4},{1,0,4},{2,0,4},{3,0,4},{4,0,4},{5,0,4},{6,0,4},{7,0,4},{8,0,4},{9,0,4},{10,0,4},{11,0,4},{12,0,4},{13,0,4},{14,0,4},{15,0,4},{16,0,4},{17,0,4},{18,0,4},{19,0,4},{19,1,5},{3,4,1},{5,4,2},{9,4,2},{13,4,2},{16,4,1},{3,5,1},{16,5,1},{3,6,1},{16,6,1},{3,7,1},{7,7,3},{15,7,3},{16,7,1},{4,9,1},{5,9,1},{6,9,1},{7,9,1},{9,9,1},{10,9,1},{11,9,1},{12,9,1},{14,9,1},{15,9,1},{16,9,1}}
		));


		mapList.Add(new ParseController.MapEntity(3,new int[,]
		                                          {{0,0,4},{1,0,4},{2,0,4},{3,0,4},{4,0,4},{5,0,4},{6,0,4},{7,0,4},{8,0,4},{9,0,4},{10,0,4},{11,0,4},{12,0,4},{13,0,4},{14,0,4},{15,0,4},{16,0,4},{17,0,4},{18,0,4},{19,0,4},{2,1,1},{10,1,1},{11,1,1},{19,1,5},{3,2,1},{4,3,1},{17,3,1},{5,4,1},{6,4,1},{7,4,1},{8,4,1},{9,4,1},{17,4,1},{9,5,2},{12,5,1},{13,5,1},{14,5,1},{15,5,1},{16,5,1},{17,5,1},{9,6,1},{12,6,1},{13,6,1},{14,6,1},{15,6,1},{16,6,1},{17,6,1},{5,7,1},{6,7,1},{9,7,1},{15,7,1},{16,7,1},{17,7,1},{5,8,1},{9,8,1},{15,8,1},{16,8,1},{17,8,1},{19,8,3},{4,9,1},{17,9,1},{3,10,1},{17,10,1},{2,11,1},{17,11,1},{1,12,1},{17,12,1},{19,12,3},{4,13,1},{5,13,1},{6,13,1},{7,13,1},{8,13,2},{9,13,1},{10,13,1},{11,13,1},{12,13,1},{13,13,1},{14,13,1}}
		));

		mapList.Add(new ParseController.MapEntity(4,new int[,]
		                                          {{0,0,4},{1,0,4},{2,0,4},{3,0,4},{4,0,4},{5,0,4},{6,0,4},{7,0,4},{8,0,4},{9,0,4},{10,0,4},{11,0,4},{12,0,4},{13,0,4},{14,0,4},{15,0,4},{16,0,4},{17,0,4},{18,0,4},{19,0,4},{2,1,1},{10,1,1},{11,1,1},{19,1,5},{3,2,1},{4,3,1},{17,3,1},{5,4,1},{6,4,1},{7,4,1},{8,4,1},{9,4,1},{17,4,1},{9,5,2},{12,5,1},{13,5,1},{14,5,1},{15,5,1},{16,5,1},{17,5,1},{9,6,1},{12,6,1},{13,6,1},{14,6,1},{15,6,1},{16,6,1},{17,6,1},{5,7,1},{6,7,1},{9,7,1},{15,7,1},{16,7,1},{17,7,1},{5,8,1},{9,8,1},{15,8,1},{16,8,1},{17,8,1},{19,8,3},{4,9,1},{17,9,1},{3,10,1},{17,10,1},{2,11,1},{17,11,1},{1,12,1},{17,12,1},{19,12,3},{4,13,1},{5,13,1},{6,13,1},{7,13,1},{8,13,2},{9,13,1},{10,13,1},{11,13,1},{12,13,1},{13,13,1},{14,13,1}}
		));

		mapList.Add(new ParseController.MapEntity(5,new int[,]
		                                          {{0,0,4},{1,0,4},{2,0,4},{3,0,4},{4,0,4},{5,0,4},{6,0,4},{7,0,4},{8,0,4},{9,0,4},{10,0,4},{11,0,4},{12,0,4},{13,0,4},{14,0,4},{15,0,4},{16,0,4},{17,0,4},{18,0,4},{19,0,4},{2,1,1},{10,1,1},{11,1,1},{19,1,5},{3,2,1},{4,3,1},{17,3,1},{5,4,1},{6,4,1},{7,4,1},{8,4,1},{9,4,1},{17,4,1},{9,5,2},{12,5,1},{13,5,1},{14,5,1},{15,5,1},{16,5,1},{17,5,1},{9,6,1},{12,6,1},{13,6,1},{14,6,1},{15,6,1},{16,6,1},{17,6,1},{5,7,1},{6,7,1},{9,7,1},{15,7,1},{16,7,1},{17,7,1},{5,8,1},{9,8,1},{15,8,1},{16,8,1},{17,8,1},{19,8,3},{4,9,1},{17,9,1},{3,10,1},{17,10,1},{2,11,1},{17,11,1},{1,12,1},{17,12,1},{19,12,3},{4,13,1},{5,13,1},{6,13,1},{7,13,1},{8,13,2},{9,13,1},{10,13,1},{11,13,1},{12,13,1},{13,13,1},{14,13,1}}
		));

		mapList.Add(new ParseController.MapEntity(6,new int[,]
		                                          {{0,0,4},{1,0,4},{2,0,4},{3,0,4},{4,0,4},{5,0,4},{6,0,4},{7,0,4},{8,0,4},{9,0,4},{10,0,4},{11,0,4},{12,0,4},{13,0,4},{14,0,4},{15,0,4},{16,0,4},{17,0,4},{18,0,4},{19,0,4},{2,1,1},{10,1,1},{11,1,1},{19,1,5},{3,2,1},{4,3,1},{17,3,1},{5,4,1},{6,4,1},{7,4,1},{8,4,1},{9,4,1},{17,4,1},{9,5,2},{12,5,1},{13,5,1},{14,5,1},{15,5,1},{16,5,1},{17,5,1},{9,6,1},{12,6,1},{13,6,1},{14,6,1},{15,6,1},{16,6,1},{17,6,1},{5,7,1},{6,7,1},{9,7,1},{15,7,1},{16,7,1},{17,7,1},{5,8,1},{9,8,1},{15,8,1},{16,8,1},{17,8,1},{19,8,3},{4,9,1},{17,9,1},{3,10,1},{17,10,1},{2,11,1},{17,11,1},{1,12,1},{17,12,1},{19,12,3},{4,13,1},{5,13,1},{6,13,1},{7,13,1},{8,13,2},{9,13,1},{10,13,1},{11,13,1},{12,13,1},{13,13,1},{14,13,1}}
		));


        RefreshButtons();
	}

    private void SetMap(int i, ParseController.MapEntity map) {
        UnityEngine.UI.Button[] buttons = new UnityEngine.UI.Button[]
        {
            map1Button,
            map2Button,
            map3Button
        };
        UnityEngine.UI.Image[] flags = new UnityEngine.UI.Image[]
        {
            flag1,
            flag2,
            flag3
        };       

        var text = map != null ? map.GetTitle() : "-";
        bool done = map != null;
        buttons [i].GetComponentInChildren<UnityEngine.UI.Text>().text =  text;
        buttons [i].enabled = map != null;
        flags [i].enabled = map != null && ParseController.HasCompletedSinglePlayerLevel(map.Number);
    }
                       

    private void RefreshButtons() {
        for (int i = 0; i < 3; i++)
        {
            var mapIndex = numButtonsPerPage * numPage + i;
            if (mapIndex < mapList.Count) {
                var map = mapList[mapIndex];
                SetMap(i, map);
            } else {
                SetMap(i, null);
            }
        }
        prevButton.enabled = numPage > 0;
        nextButton.enabled = (numPage + 1) * numButtonsPerPage < mapList.Count;
    }

   
	public void nextPageButtons() {
        numPage++;
        RefreshButtons();
	}

	public void prevPageButtons() {
		numPage--;
        RefreshButtons();
	}

	public void playMap(int i) {
        ParseController.MapEntity mapToLoad = mapList[numButtonsPerPage * numPage + (i-1)]; 

		//HORI guarrada no se que es el application loaded level
		GameController.numDies = 0;
		// igualmente no funciona
        
		if (mode == "mapeditor")
        {
            MapEditor.mapEntity = mapToLoad;
            Application.LoadLevel("Editor");
        } else
        {
            GameController.mapToLoad = mapToLoad;
            Application.LoadLevel("Game");
        }

	}

	public void goMainMenuButton() {
		Application.LoadLevel("Main");
	}


	// Update is called once per frame
	void Update () {
	
	}

}
