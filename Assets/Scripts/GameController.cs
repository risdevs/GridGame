using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public static int LevelToLoad = 1; 

	public TileRenderer tileRenderer;

    // Use this for initialization
    void Start()
    {
        //Application.LoadLevelAdditive("Level" + LevelToLoad);
        
        //Application.LoadLevelAdditive("Level" + Random.Range(1,3));


		for (int i = 0; i < GridRendering.COLS; i++)
		{
			TileRenderer tr = (TileRenderer) Instantiate (tileRenderer);
			tr.tile = new Vector3 (i, 0);
			tr.currentSprite = i % 3;
		}
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
