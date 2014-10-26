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


	ParseController.MapEntity map1;
	ParseController.MapEntity map2;
	ParseController.MapEntity map3;

    public UnityEngine.UI.Image flag1;
    public UnityEngine.UI.Image flag2;
    public UnityEngine.UI.Image flag3;

    public UnityEngine.UI.Text scoreText;

    int numButtonsPerPage=3;

    public static string previousmode;

    public static string mode;
    	
    public static List<ParseController.MapEntity> mapList;

    static int numPage = 0;

	// Use this for initialization
	void Start () {

        //ParseController.CompleteSinglePlayerLevel(0);

		//mode = "singleplayer";

		Debug.Log ("mode list maps " + mode);

        if (mode == previousmode)
        {
            RefreshButtons();
        } else
        {
            mapList = new List<ParseController.MapEntity>();
            numPage = 0;
            RefreshButtons();
            if (mode == "multiplayer" || mode == "mapeditor") 
                StartCoroutine("LoadMaps");
            else if (mode == "singleplayer")
            {
                LoadSinglePlayerMaps();
            }
        }

        previousmode = mode;
        scoreText.text = "SCORE: " + ParseController.GetScore(mode != "singleplayer");


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

	/*


CODIGO A COPIAR >>>>     {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{6,0,1},{7,0,1},{8,0,1},{9,0,1},{10,0,1},{11,0,1},{12,0,1},{13,0,1},{14,0,1},{15,0,1},{16,0,1},{17,0,1},{18,0,1},{19,0,1},{16,1,12},{19,1,5},{16,2,12},{4,3,1},{5,3,1},{6,3,1},{7,3,1},{8,3,1},{16,3,12},{19,3,13},{5,4,13},{6,4,13},{7,4,13},{13,4,3},{16,4,12},{16,5,12},{19,5,13},{8,6,1},{9,6,1},{10,6,1},{11,6,1},{12,6,1},{16,6,12},{9,7,13},{10,7,13},{11,7,13},{19,7,13},{12,9,1},{13,9,1},{14,9,1},{15,9,1},{16,9,1},{19,9,13},{13,11,13},{14,11,13},{15,11,13},{16,11,13},{13,12,13},{14,12,13},{15,12,13},{16,12,13}}
UnityEngine.Debug:Log(Object)
MapEditor:printButtonAction() (at Assets/Scripts/MapEditor.cs:156)
UnityEngine.EventSystems.EventSystem:Update()

CODIGO A COPIAR >>>>     {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{9,0,6},{10,0,6},{11,0,6},{12,0,6},{13,0,6},{14,0,6},{15,0,6},{16,0,6},{17,0,6},{19,0,1},{19,1,5},{2,2,1},{3,2,1},{4,2,1},{5,2,1},{10,2,11},{12,2,11},{14,2,11},{16,2,11},{4,4,1},{5,4,1},{6,4,1},{7,4,1},{9,4,11},{1,5,13},{2,6,13},{12,6,13},{13,6,13},{14,6,13},{15,6,13},{16,6,13},{3,7,13},{12,7,13},{13,7,13},{14,7,13},{15,7,13},{16,7,13},{4,8,13},{10,10,13},{7,11,13},{8,11,13},{9,11,13},{10,11,13},{11,11,13},{10,12,13}}
UnityEngine.Debug:Log(Object)
MapEditor:printButtonAction() (at Assets/Scripts/MapEditor.cs:156)
UnityEngine.EventSystems.EventSystem:Update()


CODIGO A COPIAR >>>>     {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{6,0,1},{7,0,1},{8,0,1},{9,0,1},{10,0,4},{11,0,4},{12,0,4},{13,0,4},{14,0,4},{15,0,4},{16,0,4},{17,0,4},{18,0,4},{19,0,1},{4,3,2},{8,3,2},{12,3,2},{15,3,12},{16,3,4},{17,3,4},{15,4,12},{15,5,12},{15,6,12},{18,6,4},{19,6,4},{15,7,12},{15,8,12},{8,9,10},{12,9,10},{15,9,12},{16,9,4},{17,9,4},{2,10,12},{3,10,12},{4,10,12},{15,10,12},{1,11,12},{3,11,5},{7,11,13},{9,11,13},{11,11,13},{15,11,12},{1,12,12},{13,12,12},{14,12,12},{15,12,12},{18,12,4},{19,12,4},{1,13,12},{2,14,12},{3,14,12},{4,14,12},{13,14,13},{14,14,13},{15,14,13},{16,14,13},{17,14,13},{18,14,13},{19,14,13}}
UnityEngine.Debug:Log(Object)
MapEditor:printButtonAction() (at Assets/Scripts/MapEditor.cs:156)
UnityEngine.EventSystems.EventSystem:Update()


*/
    void LoadSinglePlayerMaps() {
        mapList = new List<ParseController.MapEntity>();
        mapList.Add(new ParseController.MapEntity(1,new int[,]
		                                          {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{6,0,1},{7,0,1},{8,0,1},{9,0,1},{10,0,1},{11,0,1},{12,0,1},{13,0,1},{14,0,1},{15,0,1},{16,0,1},{17,0,1},{18,0,1},{19,0,1},{16,1,12},{19,1,5},{16,2,12},{4,3,1},{5,3,1},{6,3,1},{7,3,1},{8,3,1},{16,3,12},{19,3,13},{5,4,13},{6,4,13},{7,4,13},{13,4,3},{16,4,12},{16,5,12},{19,5,13},{8,6,1},{9,6,1},{10,6,1},{11,6,1},{12,6,1},{16,6,12},{9,7,13},{10,7,13},{11,7,13},{19,7,13},{12,9,1},{13,9,1},{14,9,1},{15,9,1},{16,9,1},{19,9,13},{13,11,13},{14,11,13},{15,11,13},{16,11,13},{13,12,13},{14,12,13},{15,12,13},{16,12,13}}
        ));

        mapList.Add(new ParseController.MapEntity(2,new int[,]
		                                          {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{9,0,6},{10,0,6},{11,0,6},{12,0,6},{13,0,6},{14,0,6},{15,0,6},{16,0,6},{17,0,6},{19,0,1},{19,1,5},{2,2,1},{3,2,1},{4,2,1},{5,2,1},{10,2,11},{12,2,11},{14,2,11},{16,2,11},{4,4,1},{5,4,1},{6,4,1},{7,4,1},{9,4,11},{1,5,13},{2,6,13},{12,6,13},{13,6,13},{14,6,13},{15,6,13},{16,6,13},{3,7,13},{12,7,13},{13,7,13},{14,7,13},{15,7,13},{16,7,13},{4,8,13},{10,10,13},{7,11,13},{8,11,13},{9,11,13},{10,11,13},{11,11,13},{10,12,13}}
		));


		mapList.Add(new ParseController.MapEntity(3,new int[,]
		                                          {{0,0,4},{1,0,4},{2,0,4},{3,0,4},{4,0,4},{5,0,4},{6,0,4},{7,0,4},{8,0,4},{9,0,4},{10,0,4},{11,0,4},{12,0,4},{13,0,4},{14,0,4},{15,0,4},{16,0,4},{17,0,4},{18,0,4},{19,0,4},{2,1,1},{10,1,1},{11,1,1},{19,1,5},{3,2,1},{4,3,1},{17,3,1},{5,4,1},{6,4,1},{7,4,1},{8,4,1},{9,4,1},{17,4,1},{9,5,2},{12,5,1},{13,5,1},{14,5,1},{15,5,1},{16,5,1},{17,5,1},{9,6,1},{12,6,1},{13,6,1},{14,6,1},{15,6,1},{16,6,1},{17,6,1},{5,7,1},{6,7,1},{9,7,1},{15,7,1},{16,7,1},{17,7,1},{5,8,1},{9,8,1},{15,8,1},{16,8,1},{17,8,1},{19,8,3},{4,9,1},{17,9,1},{3,10,1},{17,10,1},{2,11,1},{17,11,1},{1,12,1},{17,12,1},{19,12,3},{4,13,1},{5,13,1},{6,13,1},{7,13,1},{8,13,2},{9,13,1},{10,13,1},{11,13,1},{12,13,1},{13,13,1},{14,13,1}}
		));

		mapList.Add(new ParseController.MapEntity(4,new int[,]
		                                          {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{6,0,1},{7,0,1},{8,0,1},{9,0,1},{10,0,4},{11,0,4},{12,0,4},{13,0,4},{14,0,4},{15,0,4},{16,0,4},{17,0,4},{18,0,4},{19,0,1},{4,3,2},{8,3,2},{12,3,2},{15,3,12},{16,3,4},{17,3,4},{15,4,12},{15,5,12},{15,6,12},{18,6,4},{19,6,4},{15,7,12},{15,8,12},{8,9,10},{12,9,10},{15,9,12},{16,9,4},{17,9,4},{2,10,12},{3,10,12},{4,10,12},{15,10,12},{1,11,12},{3,11,5},{7,11,13},{9,11,13},{11,11,13},{15,11,12},{1,12,12},{13,12,12},{14,12,12},{15,12,12},{18,12,4},{19,12,4},{1,13,12},{2,14,12},{3,14,12},{4,14,12},{13,14,13},{14,14,13},{15,14,13},{16,14,13},{17,14,13},{18,14,13},{19,14,13}}
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
        flags [i].enabled = map != null && mode != "mapeditor" && map.HasBeenCompleted();
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
            GameController.sceneToBack = Application.loadedLevel;
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
