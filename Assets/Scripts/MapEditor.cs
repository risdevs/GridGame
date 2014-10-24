using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

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

	// Use this for initialization
	void Start ()
	{
		gridRendering = Camera.main.GetComponent<GridRendering> ();
		tiles = new TileRenderer[GridRendering.COLS * GridRendering.ROWS];
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
		
		int xy = ((int)tile.y) * GridRendering.COLS + ((int)tile.x);
		
		if (tiles[xy] != null)
		{
			TileRenderer tr = tiles[xy];
			tiles[xy] = null;
			tr.transform.parent = null;
			Destroy (tr.gameObject);
        }
    }

	public void SaveMap()
	{
		System.Collections.ArrayList list = new ArrayList ();

		for(int i = 0; i < tiles.Length; i++)
		{
			if(tiles[i] != null)
			{
				list.Add(new MapData(tiles[i].tile, tiles[i].currentSprite));
			}
		}

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
		bf.Serialize (file, list);
		file.Close();
	}
}
