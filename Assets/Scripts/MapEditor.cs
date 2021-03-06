﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;
using Parse;

public class MapEditor : MonoBehaviour
{
	public enum MODE
	{
		BUILD,
		DELETE
	}


	public UnityEngine.UI.Button printButton;
	public UnityEngine.UI.Button playButton;
    public UnityEngine.UI.Button modeButton;

	public MODE currentMode;

	private Sprite[] sprites;
	public int selectedTile;
	public TileRenderer tilePrefab;
	public GameObject mapRoot;

    public Button tileButtonPrefab;
    public Canvas mainCanvas;

	private GridRendering gridRendering;

	private TileRenderer[] tiles;

    public static ParseController.MapEntity mapEntity;

    	// Use this for initialization
	void Start ()
	{
        Debug.Log("START");
        gridRendering = Camera.main.GetComponent<GridRendering> ();
		tiles = new TileRenderer[GridRendering.COLS * GridRendering.ROWS];

        sprites = tilePrefab.sprites;


        bool hasFlag = false;
        LoadUI();
        foreach (ParseController.MapTile t in mapEntity.tiles) {
            TileRenderer tr = (TileRenderer) Instantiate (tilePrefab);
            tr.tile = new Vector3 (t.x, t.y);
            tr.currentSprite = t.sprite;
            tr.transform.parent = mapRoot.transform;
            int xy = ((int)t.y) * GridRendering.COLS + ((int)t.x);
            
            if(tiles[xy] != null)
            {
                DeleteTile(t.x, t.y);
            }
            tiles[xy] = tr;

            if(t.sprite == 5) hasFlag = true;
        }

        
        //Create end flag
        if (!hasFlag)
        {
            TileRenderer tr = (TileRenderer)Instantiate(tilePrefab);
            tr.tile = new Vector3(GridRendering.COLS - 1, 1);
            tr.currentSprite = 5;
            tr.transform.parent = mapRoot.transform;
            tr.gameObject.name = Utils.NAME_TILE_END_FLAG;
            tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
            tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            
            int xy = ((int)tr.tile.y) * GridRendering.COLS + ((int)tr.tile.x);
            if (tiles [xy] != null)
            {
                DeleteTile(tr.tile.x, tr.tile.y);
            }
            tiles [xy] = tr;
        }

        SetupLevel();
    }

    private void LoadUI()
    {
        Button b;
        for (int i = 1; i < sprites.Length; i++)
        {
            b = Instantiate(tileButtonPrefab) as Button;
            b.GetComponent<Image>().sprite = sprites[i];
            b.transform.parent = mainCanvas.transform;
            var col = (i-1) % 8;
            var row = (int)Mathf.Round((i-1)/8);
            b.transform.localPosition = new Vector3(50 + (sprites[i].bounds.size.x + 20) * 3 * col - 320, 
                                                    -170 - row * (sprites[i].bounds.size.y + 20) * 3 );
            b.transform.localScale = new Vector3(3,3);

            int j = i;

            Debug.Log("Adding Button with sprite:" + j);

            b.onClick.AddListener (delegate () {
                Debug.Log("sprite:" + j);
                
                selectedTile = j;
                if (currentMode == MODE.DELETE)
                {
                    SwitchMode(modeButton);
                }
            });
        }
    }



	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton (0))
		{
			if(currentMode == MODE.BUILD)
			{
				BuildTile();
			}
			else{
				RemoveTile();
			}
		}

	}

	public string convertMapToString() {
		string strinMap = "{";
		
		for(int i = 0; i < tiles.Length; i++)
		{
			if(tiles[i] != null)
			{
				strinMap = strinMap + "{" + tiles[i].tile.x + "," + tiles[i].tile.y + "," + tiles[i].currentSprite + "},";
			}
		}
		
		strinMap = strinMap.Remove(strinMap.Length - 1) + "}";

		return strinMap;
		
	}

	public void printButtonAction() {

        Debug.Log("CODIGO A COPIAR >>>>     " + convertMapToString());

	}

	public void playButtonAction(){

		GameController.mapToLoad = mapEntity;
		Application.LoadLevel("Game");

	}

    private void SetupLevel()
    {
        TileRenderer tr;
        int xy;

        
        tr = (TileRenderer) Instantiate (tilePrefab);
        tr.tile = new Vector3 (0,0);
        tr.currentSprite = 1;
        tr.transform.parent = mapRoot.transform;
        xy = ((int)tr.tile.y) * GridRendering.COLS + ((int)tr.tile.x);
        if (tiles [xy] == null)
        {
            tiles [xy] = tr;
        } else
        {
            Destroy(tr.gameObject);
        }

        
        tr = (TileRenderer) Instantiate (tilePrefab);
        tr.tile = new Vector3 (GridRendering.COLS - 1,0);
        tr.currentSprite = 1;
        tr.transform.parent = mapRoot.transform;
        xy = ((int)tr.tile.y) * GridRendering.COLS + ((int)tr.tile.x);
        if (tiles [xy] == null)
        {
            tiles [xy] = tr;
        } else
        {
            Destroy(tr.gameObject);
        }


        for (int i = 0; i < GridRendering.COLS; i++)
        {
            /*
            tr = (TileRenderer) Instantiate (tilePrefab);
            tr.tile = new Vector3 (i,0);
            tr.currentSprite = 1;
            tr.transform.parent = mapRoot.transform;
            xy = ((int)tr.tile.y) * GridRendering.COLS + ((int)tr.tile.x);
            tiles[xy] = tr;


            tr = (TileRenderer) Instantiate (tilePrefab);
            tr.tile = new Vector3 (i,-1);
            tr.currentSprite = 0;
            tr.transform.parent = mapRoot.transform;
            tr.gameObject.name = Utils.NAME_TILE_DEAD;
            tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
            tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;



            tr = (TileRenderer) Instantiate (tilePrefab);
            tr.tile = new Vector3 (i,GridRendering.ROWS);
            tr.currentSprite = 0;
            tr.transform.parent = mapRoot.transform;
            */
        }

        /*
        for (int i = 0; i < GridRendering.ROWS; i++)
        {
            tr = (TileRenderer) Instantiate (tilePrefab);
            tr.tile = new Vector3 (-1,i);
            tr.currentSprite = 0;
            tr.transform.parent = mapRoot.transform;


            tr = (TileRenderer) Instantiate (tilePrefab);
            tr.tile = new Vector3 (GridRendering.COLS, i);
            tr.currentSprite = 0;
            tr.transform.parent = mapRoot.transform;
        }
*/
    }
	
	private void BuildTile()
	{
		Vector3 tile = gridRendering.WorldToTile(Camera.main.ScreenToWorldPoint(Input.mousePosition));

		if (tile.x < 0 || tile.y < 0)
						return;

        
        Debug.Log("Build tile: " + tile);
		
		
		TileRenderer tr = (TileRenderer)Instantiate(tilePrefab);
		tr.tile = tile;
		tr.transform.parent = mapRoot.transform;
		tr.currentSprite = selectedTile;

        if (tile.x < 0 || tile.x >= GridRendering.COLS || tile.y < 0 || tile.y >= GridRendering.ROWS)
            return;

        int xy = ((int)tile.y) * GridRendering.COLS + ((int)tile.x);
        if (tiles [xy] != null)
        {
            DeleteTile(tile.x, tile.y);
        }
        tiles[xy] = tr;


        if (selectedTile == 5)
        {
            tr.gameObject.name = Utils.NAME_TILE_END_FLAG;
            tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
            tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
	}
	
	private void RemoveTile()
	{
		Vector3 tile = gridRendering.WorldToTile(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		
		if (tile.x < 0 || tile.y < 0 || tile.x >= GridRendering.COLS || tile.y >= GridRendering.ROWS)
            return;

        Debug.Log("Remove tile: " + tile);
        
		int xy = ((int)tile.y) * GridRendering.COLS + ((int)tile.x);
		
		if (tiles[xy] != null)
		{
            DeleteTile(tile.x, tile.y);
        }
    }

    private void DeleteTile(float x, float y)
    {
        int xy = ((int)y) * GridRendering.COLS + ((int)x);

        TileRenderer tr = tiles[xy];
        tiles[xy] = null;
        tr.transform.parent = null;
        Destroy (tr.gameObject);
    }

    public void UpdateMapEntity() {
        mapEntity.tiles.Clear();
        Debug.Log("TILE: " + tiles.Length);
        for(int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i] != null)
            {
                ParseController.MapTile t = new ParseController.MapTile();
                t.x = tiles[i].tile.x;
                t.y = tiles[i].tile.y;
                t.sprite = tiles[i].currentSprite;
                mapEntity.tiles.Add(t);
            }
        }
    }

	public void SaveMap()
	{
        UpdateMapEntity();
        mapEntity.Save();

	}

	public void SwitchMode(UnityEngine.UI.Button b)
	{
		if (currentMode == MODE.BUILD)
		{
			currentMode = MODE.DELETE;
			b.GetComponentInChildren<UnityEngine.UI.Text> ().text = "BUILD";
		} else {
			currentMode = MODE.BUILD;
			b.GetComponentInChildren<UnityEngine.UI.Text> ().text = "DELETE";
		}
  	}
    public void BackToMainMenu()
	{
		Application.LoadLevel("Main");
	}

    public void PlayLevel()
    {
        // TODO: ANDRES obrir el joc amb el mapa que estem editant
        this.UpdateMapEntity();
        GameController.mapToLoad = MapEditor.mapEntity;
        GameController.sceneToBack = Application.loadedLevel;
        Application.LoadLevel("Game");
    }

    public void PrintMatrix()
    {
        // TODO: ANDRES mostar per pantalla o per debug el array
    }
}
