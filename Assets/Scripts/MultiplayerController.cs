﻿using UnityEngine;
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
    public UnityEngine.UI.Text levelCountText;

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



CODIGO A COPIAR >>>>     {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{6,0,1},{7,0,1},{8,0,1},{9,0,1},{10,0,1},{11,0,1},{12,0,1},{13,0,1},{14,0,1},{15,0,1},{16,0,1},{17,0,1},{18,0,1},{19,0,1},{5,1,16},{7,1,13},{12,1,13},{17,1,13},{19,1,5},{7,4,1},{8,4,1},{9,4,1},{10,4,1},{11,4,1},{12,4,1},{13,4,1},{14,4,1},{15,4,1},{16,4,1},{17,4,1},{18,4,1},{19,4,1},{7,5,13},{8,5,13},{9,5,13},{10,5,13},{11,5,13},{14,5,13},{15,5,13},{16,5,13},{17,5,13},{1,6,1},{7,6,15},{8,6,15},{9,6,15},{10,6,15},{11,6,15},{14,6,15},{15,6,15},{16,6,15},{17,6,15},{0,7,1},{1,7,1},{2,7,1},{3,7,1},{4,7,1},{7,7,1},{8,7,1},{9,7,1},{10,7,1},{11,7,1},{14,7,1},{15,7,1},{16,7,1},{17,7,1},{18,7,1},{0,8,1},{8,8,16},{9,8,16},{10,8,16},{16,8,16},{4,10,1},{5,10,1},{6,10,1},{7,10,1},{11,10,1},{12,10,1},{13,10,1},{14,10,1},{15,10,1},{1,12,13},{2,12,13},{4,12,13},{5,12,13},{7,12,13},{8,12,13},{10,12,13},{11,12,13},{13,12,13},{14,12,13},{16,12,13},{17,12,13},{1,13,13},{2,13,13},{4,13,13},{5,13,13},{7,13,13},{8,13,13},{10,13,13},{11,13,13},{13,13,13},{14,13,13},{16,13,13},{17,13,13}}
UnityEngine.Debug:Log(Object)
MapEditor:printButtonAction() (at Assets/Scripts/MapEditor.cs:156)
UnityEngine.EventSystems.EventSystem:Update()


CODIGO A COPIAR >>>>     {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{6,0,1},{7,0,1},{8,0,1},{9,0,1},{10,0,1},{11,0,1},{12,0,1},{13,0,1},{14,0,1},{15,0,1},{16,0,1},{17,0,1},{18,0,1},{19,0,1},{19,1,5},{3,7,3},{4,7,3},{7,7,3},{8,7,3},{9,7,3},{11,7,3},{12,7,3},{13,7,3},{15,7,3},{16,7,3},{17,7,3},{3,8,3},{5,8,3},{7,8,3},{9,8,3},{13,8,3},{17,8,3},{3,9,3},{4,9,3},{7,9,3},{9,9,3},{11,9,3},{12,9,3},{13,9,3},{15,9,3},{16,9,3},{17,9,3},{3,10,3},{5,10,3},{7,10,3},{9,10,3},{11,10,3},{15,10,3},{3,11,3},{4,11,3},{7,11,3},{8,11,3},{9,11,3},{11,11,3},{12,11,3},{13,11,3},{15,11,3},{16,11,3},{17,11,3}}
UnityEngine.Debug:Log(Object)
MapEditor:printButtonAction() (at Assets/Scripts/MapEditor.cs:156)
UnityEngine.EventSystems.EventSystem:Update()

CODIGO A COPIAR >>>>     {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{13,0,1},{14,0,1},{15,0,1},{16,0,1},{17,0,1},{18,0,1},{19,0,1},{5,1,1},{19,1,1},{5,2,1},{6,2,1},{7,2,1},{8,2,1},{9,2,1},{10,2,1},{11,2,1},{12,2,1},{13,2,1},{19,2,1},{1,3,13},{4,3,1},{5,3,1},{16,3,13},{18,3,1},{19,3,1},{1,4,13},{5,4,1},{16,4,13},{19,4,1},{1,5,13},{5,5,1},{7,5,1},{8,5,1},{9,5,1},{10,5,1},{11,5,1},{12,5,1},{13,5,1},{16,5,13},{19,5,1},{1,6,13},{4,6,1},{5,6,1},{13,6,1},{16,6,13},{18,6,1},{19,6,1},{1,7,13},{5,7,1},{13,7,1},{16,7,13},{19,7,1},{1,8,13},{5,8,1},{6,8,1},{7,8,1},{8,8,1},{9,8,1},{10,8,1},{11,8,1},{13,8,1},{16,8,13},{19,8,1},{1,9,13},{4,9,1},{5,9,1},{13,9,1},{16,9,13},{18,9,1},{19,9,1},{1,10,13},{5,10,1},{13,10,1},{16,10,13},{19,10,1},{1,11,13},{5,11,1},{7,11,12},{8,11,12},{9,11,12},{10,11,12},{11,11,1},{12,11,1},{13,11,1},{14,11,1},{16,11,13},{19,11,1},{1,12,13},{4,12,1},{5,12,1},{7,12,12},{9,12,5},{16,12,13},{18,12,1},{19,12,1},{5,13,1},{7,13,12},{19,13,1},{8,14,16},{9,14,16},{10,14,16},{11,14,16},{12,14,16},{13,14,16}}
UnityEngine.Debug:Log(Object)
MapEditor:printButtonAction() (at Assets/Scripts/MapEditor.cs:156)
UnityEngine.EventSystems.EventSystem:Update()


CODIGO A COPIAR >>>>     {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{6,0,1},{7,0,1},{8,0,1},{9,0,1},{10,0,1},{11,0,1},{12,0,1},{13,0,1},{14,0,1},{15,0,1},{16,0,1},{17,0,1},{18,0,1},{19,0,1},{19,1,5},{2,7,3},{4,7,3},{6,7,3},{7,7,3},{8,7,3},{10,7,3},{12,7,3},{14,7,3},{2,8,3},{4,8,3},{6,8,3},{8,8,3},{10,8,3},{12,8,3},{14,8,3},{2,9,3},{3,9,3},{4,9,3},{6,9,3},{8,9,3},{10,9,3},{11,9,3},{14,9,3},{2,10,3},{4,10,3},{6,10,3},{8,10,3},{10,10,3},{12,10,3},{14,10,3},{2,11,3},{4,11,3},{6,11,3},{7,11,3},{8,11,3},{10,11,3},{11,11,3},{12,11,3},{14,11,3}}
UnityEngine.Debug:Log(Object)
MapEditor:printButtonAction() (at Assets/Scripts/MapEditor.cs:156)
UnityEngine.EventSystems.EventSystem:Update()

CODIGO A COPIAR >>>>     {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{6,0,1},{7,0,1},{8,0,1},{9,0,1},{10,0,1},{11,0,1},{12,0,1},{13,0,1},{14,0,1},{15,0,1},{16,0,1},{17,0,1},{18,0,1},{19,0,1},{19,1,5},{2,7,3},{4,7,3},{6,7,3},{7,7,3},{8,7,3},{10,7,3},{12,7,3},{14,7,3},{2,8,3},{4,8,3},{6,8,3},{8,8,3},{10,8,3},{12,8,3},{14,8,3},{2,9,3},{3,9,3},{4,9,3},{6,9,3},{8,9,3},{10,9,3},{11,9,3},{14,9,3},{2,10,3},{4,10,3},{6,10,3},{8,10,3},{10,10,3},{12,10,3},{14,10,3},{2,11,3},{4,11,3},{6,11,3},{7,11,3},{8,11,3},{10,11,3},{11,11,3},{12,11,3},{14,11,3}}
UnityEngine.Debug:Log(Object)
MapEditor:printButtonAction() (at Assets/Scripts/MapEditor.cs:156)
UnityEngine.EventSystems.EventSystem:Update()


CODIGO A COPIAR >>>>     {{0,0,1},{1,0,1},{5,0,11},{8,0,11},{10,0,11},{12,0,11},{13,0,11},{14,0,11},{18,0,1},{19,0,1},{5,1,11},{7,1,11},{10,1,11},{14,1,11},{19,1,5},{5,2,11},{6,2,11},{10,2,11},{12,2,11},{13,2,11},{14,2,11},{5,3,11},{7,3,11},{10,3,11},{12,3,11},{5,4,11},{6,4,11},{7,4,11},{10,4,11},{12,4,11},{13,4,11},{14,4,11},{5,10,2},{10,10,2},{15,10,2}}
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
		                                          {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{6,0,1},{7,0,1},{8,0,1},{9,0,1},{10,0,1},{11,0,1},{12,0,1},{13,0,1},{14,0,1},{15,0,1},{16,0,1},{17,0,1},{18,0,1},{19,0,1},{5,1,16},{7,1,13},{12,1,13},{17,1,13},{19,1,5},{7,4,1},{8,4,1},{9,4,1},{10,4,1},{11,4,1},{12,4,1},{13,4,1},{14,4,1},{15,4,1},{16,4,1},{17,4,1},{18,4,1},{19,4,1},{7,5,13},{8,5,13},{9,5,13},{10,5,13},{11,5,13},{14,5,13},{15,5,13},{16,5,13},{17,5,13},{1,6,1},{7,6,15},{8,6,15},{9,6,15},{10,6,15},{11,6,15},{14,6,15},{15,6,15},{16,6,15},{17,6,15},{0,7,1},{1,7,1},{2,7,1},{3,7,1},{4,7,1},{7,7,1},{8,7,1},{9,7,1},{10,7,1},{11,7,1},{14,7,1},{15,7,1},{16,7,1},{17,7,1},{18,7,1},{0,8,1},{8,8,16},{9,8,16},{10,8,16},{16,8,16},{4,10,1},{5,10,1},{6,10,1},{7,10,1},{11,10,1},{12,10,1},{13,10,1},{14,10,1},{15,10,1},{1,12,13},{2,12,13},{4,12,13},{5,12,13},{7,12,13},{8,12,13},{10,12,13},{11,12,13},{13,12,13},{14,12,13},{16,12,13},{17,12,13},{1,13,13},{2,13,13},{4,13,13},{5,13,13},{7,13,13},{8,13,13},{10,13,13},{11,13,13},{13,13,13},{14,13,13},{16,13,13},{17,13,13}}
		));

		mapList.Add(new ParseController.MapEntity(6,new int[,]
		                                          {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{6,0,1},{7,0,1},{8,0,1},{9,0,1},{10,0,1},{11,0,1},{12,0,1},{13,0,1},{14,0,1},{15,0,1},{16,0,1},{17,0,1},{18,0,1},{19,0,1},{19,1,5},{3,7,3},{4,7,3},{7,7,3},{8,7,3},{9,7,3},{11,7,3},{12,7,3},{13,7,3},{15,7,3},{16,7,3},{17,7,3},{3,8,3},{5,8,3},{7,8,3},{9,8,3},{13,8,3},{17,8,3},{3,9,3},{4,9,3},{7,9,3},{9,9,3},{11,9,3},{12,9,3},{13,9,3},{15,9,3},{16,9,3},{17,9,3},{3,10,3},{5,10,3},{7,10,3},{9,10,3},{11,10,3},{15,10,3},{3,11,3},{4,11,3},{7,11,3},{8,11,3},{9,11,3},{11,11,3},{12,11,3},{13,11,3},{15,11,3},{16,11,3},{17,11,3}}
		));


		mapList.Add(new ParseController.MapEntity(7,new int[,]
		                                          {{0,0,1},{3,0,9},{4,0,9},{6,0,12},{7,0,12},{9,0,9},{10,0,9},{12,0,12},{13,0,12},{15,0,9},{16,0,9},{19,0,1},{6,1,12},{7,1,12},{12,1,12},{13,1,12},{19,1,5},{6,2,12},{7,2,12},{12,2,12},{13,2,12},{6,3,12},{7,3,12},{12,3,12},{13,3,12},{3,4,13},{4,4,13},{6,4,16},{7,4,12},{9,4,13},{10,4,13},{12,4,12},{13,4,16},{15,4,13},{16,4,13},{3,5,13},{4,5,13},{7,5,16},{9,5,13},{10,5,13},{12,5,16},{15,5,13},{16,5,13},{3,6,13},{4,6,13},{9,6,13},{10,6,13},{15,6,13},{16,6,13},{3,7,13},{4,7,13},{9,7,13},{10,7,13},{15,7,13},{16,7,13},{4,8,13},{5,8,13},{8,8,13},{9,8,13},{10,8,13},{11,8,13},{14,8,13},{15,8,13},{5,9,13},{6,9,13},{7,9,13},{8,9,13},{11,9,13},{12,9,13},{13,9,13},{14,9,13},{6,10,13},{7,10,13},{12,10,13},{13,10,13},{7,11,13},{12,11,13}}
		));

		mapList.Add(new ParseController.MapEntity(8,new int[,]
		                                          {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{13,0,1},{14,0,1},{15,0,1},{16,0,1},{17,0,1},{18,0,1},{19,0,1},{5,1,1},{19,1,1},{5,2,1},{6,2,1},{7,2,1},{8,2,1},{9,2,1},{10,2,1},{11,2,1},{12,2,1},{13,2,1},{19,2,1},{1,3,13},{4,3,1},{5,3,1},{16,3,13},{18,3,1},{19,3,1},{1,4,13},{5,4,1},{16,4,13},{19,4,1},{1,5,13},{5,5,1},{7,5,1},{8,5,1},{9,5,1},{10,5,1},{11,5,1},{12,5,1},{13,5,1},{16,5,13},{19,5,1},{1,6,13},{4,6,1},{5,6,1},{13,6,1},{16,6,13},{18,6,1},{19,6,1},{1,7,13},{5,7,1},{13,7,1},{16,7,13},{19,7,1},{1,8,13},{5,8,1},{6,8,1},{7,8,1},{8,8,1},{9,8,1},{10,8,1},{11,8,1},{13,8,1},{16,8,13},{19,8,1},{1,9,13},{4,9,1},{5,9,1},{13,9,1},{16,9,13},{18,9,1},{19,9,1},{1,10,13},{5,10,1},{13,10,1},{16,10,13},{19,10,1},{1,11,13},{5,11,1},{7,11,12},{8,11,12},{9,11,12},{10,11,12},{11,11,1},{12,11,1},{13,11,1},{14,11,1},{16,11,13},{19,11,1},{1,12,13},{4,12,1},{5,12,1},{7,12,12},{9,12,5},{16,12,13},{18,12,1},{19,12,1},{5,13,1},{7,13,12},{19,13,1},{8,14,16},{9,14,16},{10,14,16},{11,14,16},{12,14,16},{13,14,16}}
		));


		mapList.Add(new ParseController.MapEntity(9,new int[,]
		                                          {{0,0,1},{1,0,1},{2,0,1},{3,0,1},{4,0,1},{5,0,1},{6,0,1},{7,0,1},{8,0,1},{9,0,1},{10,0,1},{11,0,1},{12,0,1},{13,0,1},{14,0,1},{15,0,1},{16,0,1},{17,0,1},{18,0,1},{19,0,1},{19,1,5},{2,7,3},{4,7,3},{6,7,3},{7,7,3},{8,7,3},{10,7,3},{12,7,3},{14,7,3},{2,8,3},{4,8,3},{6,8,3},{8,8,3},{10,8,3},{12,8,3},{14,8,3},{2,9,3},{3,9,3},{4,9,3},{6,9,3},{8,9,3},{10,9,3},{11,9,3},{14,9,3},{2,10,3},{4,10,3},{6,10,3},{8,10,3},{10,10,3},{12,10,3},{14,10,3},{2,11,3},{4,11,3},{6,11,3},{7,11,3},{8,11,3},{10,11,3},{11,11,3},{12,11,3},{14,11,3}}
		));

		mapList.Add(new ParseController.MapEntity(10,new int[,]
		                                          {{0,0,1},{1,0,1},{5,0,11},{8,0,11},{10,0,11},{12,0,11},{13,0,11},{14,0,11},{18,0,1},{19,0,1},{5,1,11},{7,1,11},{10,1,11},{14,1,11},{19,1,5},{5,2,11},{6,2,11},{10,2,11},{12,2,11},{13,2,11},{14,2,11},{5,3,11},{7,3,11},{10,3,11},{12,3,11},{5,4,11},{6,4,11},{7,4,11},{10,4,11},{12,4,11},{13,4,11},{14,4,11},{5,10,2},{10,10,2},{15,10,2}}
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

        levelCountText.text = "Levels: " + mapList.Count;
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
