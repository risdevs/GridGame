using UnityEngine;
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

	public MODE currentMode;

	public Sprite[] sprites;
	public int selectedTile;
	public TileRenderer tilePrefab;
	public GameObject mapRoot;

    public Button tileButtonPrefab;
    public Canvas mainCanvas;

	private GridRendering gridRendering;

	private TileRenderer[] tiles;

    private ParseController.MapEntity mapEntity;

    	// Use this for initialization
	void Start ()
	{
        Debug.Log("START");
        gridRendering = Camera.main.GetComponent<GridRendering> ();
		tiles = new TileRenderer[GridRendering.COLS * GridRendering.ROWS];
        mapEntity = new ParseController.MapEntity();
        StartCoroutine("LoadMap");

        LoadUI();
	}

    private void LoadUI()
    {
        Resolution res = Screen.currentResolution;
        Button b;
        for (int i = 0; i < sprites.Length; i++)
        {
            b = Instantiate(tileButtonPrefab) as Button;
            b.GetComponent<Image>().sprite = sprites[i];
            b.transform.parent = mainCanvas.transform;
            b.transform.Translate(new Vector3(res.width * 0.05f + (sprites[i].bounds.size.x + 10) * 5 * i, res.height * 0.3f));
            b.transform.localScale = new Vector3(5,5);


            int j = i;
            b.onClick.AddListener (delegate () {
                Debug.Log("sprite:" + j);

                selectedTile = j;
            });
        }
    }


    IEnumerator LoadMap() {
        Debug.Log("LOADMAP");

        ParseController.ListMapOperation list = new ParseController.ListMapOperation();
        list.run();
        while (!list.IsCompleted)
            yield return null;
            
        foreach (ParseController.MapEntity map in list.result)
        {
            Debug.Log(map.parseObject.ObjectId);
            foreach (ParseController.MapTile t in map.tiles) {
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
            }
            
            SetupLevel();
            mapEntity = map;
            break;
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

	public void printButtonAction() {

		/*
		List<List<float>> list = new List<List<float>>();
		foreach (MapTile t in tiles)
		{
			list.Add(new List<float>(){t.x, t.y, t.sprite});
		}
		
		parseObject ["map"] = list;
		parseObject.SaveAsync();
		*/


		/*

		List<List<float>> list = new List<List<float>>();
		//String que luego copiaremos para single Player
		string strinMap = "listLevelsSPM.Add(new ParseController.MapEntity('MAP_NAME_HERE',new int[,]{";
		
		foreach (MapTile t in tiles)
		{
			list.Add(new List<float>(){t.x, t.y, t.sprite});
			strinMap = strinMap + "{" + t.x + "," + t.y + "," +  t.sprite + "},";
		}
		
		strinMap = strinMap.Remove(strinMap.Length - 1) + "}));";
		//parseObject ["map"] = list;
		//parseObject.SaveAsync();
*/

	}

	public void playButtonAction(){
		GameController.mapToLoad = mapEntity;
		Application.LoadLevel("Game");

	}



    private void SetupLevel()
    {
        TileRenderer tr;
        int xy;

        
        //Create end flag
        tr = (TileRenderer) Instantiate (tilePrefab);
        tr.tile = new Vector3 (GridRendering.COLS - 1, 1);
        tr.currentSprite = 4;
        tr.transform.parent = mapRoot.transform;
        tr.gameObject.name = Utils.NAME_TILE_END_FLAG;
        tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
        tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

        xy = ((int)tr.tile.y) * GridRendering.COLS + ((int)tr.tile.x);
        if(tiles[xy] != null)
        {
            DeleteTile(tr.tile.x, tr.tile.y);
        }
        tiles[xy] = tr;


        for (int i = 0; i < GridRendering.COLS; i++)
        {
            tr = (TileRenderer) Instantiate (tilePrefab);
            tr.tile = new Vector3 (i,-1);
            tr.currentSprite = 4;
            tr.transform.parent = mapRoot.transform;
            tr.gameObject.name = Utils.NAME_TILE_DEAD;
            tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
            tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;



            
            tr = (TileRenderer) Instantiate (tilePrefab);
            tr.tile = new Vector3 (i,GridRendering.ROWS);
            tr.currentSprite = 4;
            tr.transform.parent = mapRoot.transform;
        }

        for (int i = 0; i < GridRendering.ROWS; i++)
        {
            tr = (TileRenderer) Instantiate (tilePrefab);
            tr.tile = new Vector3 (-1,i);
            tr.currentSprite = 4;
            tr.transform.parent = mapRoot.transform;


            tr = (TileRenderer) Instantiate (tilePrefab);
            tr.tile = new Vector3 (GridRendering.COLS, i);
            tr.currentSprite = 4;
            tr.transform.parent = mapRoot.transform;
        }

    }
	
	private void BuildTile()
	{
		Vector3 tile = gridRendering.WorldToTile(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		Debug.Log("tile: " + tile);

		if (tile.x < 0 || tile.y < 0)
						return;
		
		int xy = ((int)tile.y) * GridRendering.COLS + ((int)tile.x);
		

		if (tiles [xy] != null)
        {
            DeleteTile(tile.x, tile.y);
        }
		TileRenderer tr = (TileRenderer)Instantiate(tilePrefab);
		tr.tile = tile;
		tr.transform.parent = mapRoot.transform;
		tr.currentSprite = selectedTile;
        tiles[xy] = tr;
	}
	
	private void RemoveTile()
	{
		Vector3 tile = gridRendering.WorldToTile(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		Debug.Log("tile: " + tile);
		
		if (tile.x < 0 || tile.y < 0)
            return;
        
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

	public void SaveMap()
	{
        mapEntity.tiles.Clear();
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
        mapEntity.Save();

	}

	public void SwitchMode(UnityEngine.UI.Button b)
	{
		if (currentMode == MODE.BUILD)
		{
			currentMode = MODE.DELETE;
			b.GetComponentInChildren<UnityEngine.UI.Text> ().text = "DELETE";
		} else {
			currentMode = MODE.BUILD;
			b.GetComponentInChildren<UnityEngine.UI.Text> ().text = "BUILD";
		}
	}

	public void BackToMainMenu()
	{
		Application.LoadLevel("Main");
	}

    public void PlayLevel()
    {
        // TODO: ANDRES obrir el joc amb el mapa que estem editant
    }

    public void PrintMatrix()
    {
        // TODO: ANDRES mostar per pantalla o per debug el array
    }
}
