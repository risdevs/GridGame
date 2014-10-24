using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public TileRenderer tileRenderer;
	public GameObject mapRoot;

    // Use this for initialization
    void Start()
    {
		LoadLevel ();
    }
    
    // Update is called once per frame
    void Update()
    {
    }

	private void LoadLevel()
	{
		//Replace for a proper level loading
		for (int i = 0; i < GridRendering.COLS; i++)
		{
			TileRenderer tr = (TileRenderer) Instantiate (tileRenderer);
			tr.tile = new Vector3 (i, 0);
            tr.currentSprite = i % 3;
			tr.transform.parent = mapRoot.transform;
        }
	}
}
