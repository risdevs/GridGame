using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Parse;

public class MapEditor : MonoBehaviour
{
	public enum MODE
	{
		BUILD,
		DELETE
	}
	public MODE currentMode;

	public Sprite[] sprites;
	public int selectedTile;
	public TileRenderer tilePrefab;
	public GameObject mapRoot;

	private GridRendering gridRendering;

	private TileRenderer[] tiles;


	private ParseObject mapObject;

	private bool mapLoaded = false;

	// Use this for initialization
	void Start ()
	{
		gridRendering = Camera.main.GetComponent<GridRendering> ();
		tiles = new TileRenderer[GridRendering.COLS * GridRendering.ROWS];

		LoadLevel ();
	}
	
	private void LoadLevel()
	{

		Debug.Log ("LOAD LEVEL");
		ParseObject.GetQuery ("MapBytes").GetAsync ("z820YEC0OE").ContinueWith (t => {
			if (t.IsFaulted) {
				Debug.Log("ERROR");
			}
			Debug.Log ("LOAD LEVEL START");
			mapObject = t.Result;
   		});
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

		if (mapObject != null && !mapLoaded) {
			foreach (object tileobj in mapObject.Get<List<object>>("map")) {
				List<object> tile = (List<object>)tileobj;
				Debug.Log ("LOAD LEVEL TILE "  + tile[0].ToString());
				float x = float.Parse(tile[0].ToString());
				float y = float.Parse(tile[1].ToString());
				int sprite = int.Parse(tile[2].ToString());
				Debug.Log ("LOAD LEVEL TILE " + x + " " + y + " " + sprite);
				//TODO: SERGI ESTO NO FUNCIONA DESDE ESTE THREAD
				TileRenderer tr = (TileRenderer) Instantiate (tilePrefab);
				tr.tile = new Vector3 (x, y);
				tr.currentSprite = sprite;
				tr.transform.parent = mapRoot.transform;
				int xy = ((int)y) * GridRendering.COLS + ((int)x);

                if(tiles[xy] != null)
                {
                    DeleteTile(x, y);
                }
				tiles[xy] = tr;
			}
            
            SetupLevel();
            mapLoaded = true;
		}
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

        /*
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
        */
    }
	
	private void BuildTile()
	{
		Vector3 tile = gridRendering.WorldToTile(Camera.main.ScreenToWorldPoint(Input.mousePosition));
		Debug.Log("tile: " + tile);

		if (tile.x < 0 || tile.y < 0)
						return;
		
		int xy = ((int)tile.y) * GridRendering.COLS + ((int)tile.x);
		
		if (tiles[xy] == null)
		{
			TileRenderer tr = (TileRenderer)Instantiate(tilePrefab);
			tr.tile = tile;
			tr.transform.parent = mapRoot.transform;
			tr.currentSprite = selectedTile;
            tiles[xy] = tr;
        }
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

	public void ChangeTile(UnityEngine.UI.Button b)
	{
		selectedTile++;
		selectedTile %= sprites.Length;
		b.GetComponentInChildren<UnityEngine.UI.Text> ().text = "" + selectedTile;
	}

	public void SaveMap()
	{
		List<List<float>> list = new List<List<float>> ();

		for(int i = 0; i < tiles.Length; i++)
		{
			if(tiles[i] != null)
			{
				list.Add(new List<float>(){tiles[i].tile.x, tiles[i].tile.y, tiles[i].currentSprite});
			}
		}

		mapObject ["map"] = list;
		mapObject.SaveAsync ();

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
}
