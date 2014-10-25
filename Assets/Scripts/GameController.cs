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

    // Use this for initialization
    void Start()
	{
		//LoadBasicLevel ();
		LoadLevel ();
    }
    
    // Update is called once per frame
    void Update()
    {
    }

	private void LoadLevel()
	{
		if (File.Exists (Utils.GetSaveDataFile()))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Utils.GetSaveDataFile(), FileMode.Open);
			ArrayList list = (ArrayList) bf.Deserialize(file);
			file.Close();

			foreach (MapData md in list)
			{
				Debug.Log("MD pos:" + md.position + " sprite:" + md.sprite);
				TileRenderer tr = (TileRenderer) Instantiate (tileRenderer);
				tr.tile = new Vector3 (md.x, md.y);
				tr.currentSprite = md.sprite;
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
			}
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
