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
        if (mode=="multiplayer" || mode == "mapeditor") 
            StartCoroutine("LoadMaps");
		else if (mode == "singleplayer") 
            LoadSinglePlayerMaps();
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
        mapList.Add(new ParseController.MapEntity(77777,new int[,]{{7,5,0},{8,5,0},{10,5,0},{11,5,0},{12,5,0},{4,6,0},{5,6,0},{12,6,0},{4,7,0},{12,7,0},{4,8,0},{12,8,0},{4,10,0},{5,11,0},{6,12,0},{7,12,0},{7,13,0}}));

        mapList.Add(new ParseController.MapEntity(1,new int[,]
		      {{11,3,0},{12,3,0},{11,4,0},{12,4,0},{13,4,0},{10,5,0},{13,5,0},{9,6,0},{14,6,0},{9,7,0},{14,7,0},{9,8,0},{10,8,0},{11,8,0},{12,8,0},{13,8,0},{10,9,0}})
		);

        RefreshButtons();
	}

    private void SetMap(int i, ParseController.MapEntity map, bool completed) {
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
        flags [i].enabled = completed;
    }
                       

    private void RefreshButtons() {
        for (int i = 0; i < 3; i++)
        {
            var mapIndex = numButtonsPerPage * numPage + i;
            if (mapIndex < mapList.Count) {
                var map = mapList[mapIndex];
                SetMap(i, map, ParseController.HasCompletedSinglePlayerLevel(mapIndex));
            } else {
                SetMap(i, null, false);
            }
        }
        prevButton.enabled = numPage < 0;
        nextButton.enabled = (numPage + 1) * numButtonsPerPage < mapList.Count;
        nextButton.enabled = true;
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
