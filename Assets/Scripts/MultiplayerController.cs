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
             {{0,0,4},{1,0,4},{2,0,4},{3,0,4},{4,0,4},{5,0,4},{6,0,4},{7,0,4},{8,0,4},{9,0,4},{10,0,4},{11,0,4},{12,0,4},{13,0,4},{14,0,4},{15,0,4},{16,0,4},{17,0,4},{18,0,4},{19,0,4},{0,1,4},{5,1,1},{6,1,1},{7,1,1},{8,1,1},{9,1,1},{10,1,1},{19,1,5},{6,2,1},{7,2,1},{8,2,1},{9,2,1},{10,2,1},{7,3,1},{8,3,1},{9,3,1},{14,3,2},{17,3,2},{18,3,2},{19,3,2},{15,6,3},{4,11,3}}
        ));

        mapList.Add(new ParseController.MapEntity(2,new int[,]
		      {{11,3,0},{12,3,0},{11,4,0},{12,4,0},{13,4,0},{10,5,0},{13,5,0},{9,6,0},{14,6,0},{9,7,0},{14,7,0},{9,8,0},{10,8,0},{11,8,0},{12,8,0},{13,8,0},{10,9,0}})
		);

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
