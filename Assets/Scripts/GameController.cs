using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


public class GameController : MonoBehaviour
{
	public TileRenderer tileRenderer;
	public GameObject mapRoot;
	public GameObject player;
    
    public UnityEngine.UI.Button b;

    // Use this for initialization
    void Start()
      	{
    }
    
    // Update is called once per frame
    void Update()
    {
    }

    public void clickDown()
    {
        Debug.Log("clickDown");
    }

    IEnumerator LoadMap()
    {


        ParseController.ListMapOperation list = new ParseController.ListMapOperation();
        list.run();
        while (!list.IsCompleted)
            yield return null;
        
        foreach (ParseController.MapEntity map in list.result)
        {
            Debug.Log(map.parseObject.ObjectId);
            foreach (ParseController.MapTile t in map.tiles) {
                TileRenderer tr = (TileRenderer) Instantiate (tileRenderer);
                tr.tile = new Vector3 (t.x, t.y);
                tr.currentSprite = t.sprite;
                tr.transform.parent = mapRoot.transform;
                if (tr.currentSprite == 3)
                {
                    BlockFollower follower = tr.gameObject.AddComponent("BlockFollower") as BlockFollower;
                    follower.target = player;
                }
                
                
                if (tr.currentSprite == 2)
                {
                    BlockFireballs fireballs = tr.gameObject.AddComponent("BlockFireballs") as BlockFireballs;
                }
                if (tr.currentSprite == 2)
                {
                    tr.gameObject.AddComponent("BlockFireballs");
                }
            }
            //mapEntity = map;
            break;
        }

	}

	private void LoadBasicLevel()
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
