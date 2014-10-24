using UnityEngine;
using System.Collections;

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
}
