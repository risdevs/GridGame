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

		LoadLevel ();
	}
	
	private void LoadLevel()
	{
		if (File.Exists (GetSaveDataFile()))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (GetSaveDataFile(), FileMode.Open);
			ArrayList list = (ArrayList) bf.Deserialize(file);
			file.Close();
			
			foreach (MapData md in list)
			{
				Debug.Log("MD pos:" + md.position + " sprite:" + md.sprite);
				TileRenderer tr = (TileRenderer) Instantiate (tilePrefab);
				tr.tile = new Vector3 (md.x, md.y);
                tr.currentSprite = md.sprite;
                tr.transform.parent = mapRoot.transform;

				int xy = ((int)md.y) * GridRendering.COLS + ((int)md.x);
				tiles[xy] = tr;
            }
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
			TileRenderer tr = tiles[xy];
			tiles[xy] = null;
			tr.transform.parent = null;
			Destroy (tr.gameObject);
        }
    }

	public void ChangeTile(UnityEngine.UI.Button b)
	{
		selectedTile++;
		selectedTile %= sprites.Length;
		b.GetComponentInChildren<UnityEngine.UI.Text> ().text = "" + selectedTile;
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
		FileStream file = File.Create (GetSaveDataFile());
		bf.Serialize (file, list);
		file.Close();
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

	private string GetSaveDataFile()
	{
		if(Application.platform == RuntimePlatform.IPhonePlayer)
		{
			return  Utils.GetiPhoneDocumentsPath() + "/savedGames.gd";
		} else{
			return Application.persistentDataPath + "/savedGames.gd";
		}
	}
}
