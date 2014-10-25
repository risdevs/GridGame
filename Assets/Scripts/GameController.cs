﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.IO;


public class GameController : MonoBehaviour
{
	public TileRenderer tileRenderer;
	public GameObject mapRoot;
	public GameObject player;

    public Text gameText;
    
    public UnityEngine.UI.Button b;

    public static ParseController.MapEntity mapToLoad = null;

    private CharacterController2D controller;

    // Use this for initialization
    void Start()
    {
        controller = player.GetComponent<CharacterController2D>();
        controller.onTriggerEnterEvent += onTriggerEnterEvent;
        controller.onTriggerExitEvent += onTriggerExitEvent;


        foreach (ParseController.MapTile t in mapToLoad.tiles) {
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
                tr.gameObject.AddComponent("BlockFireballs");
            }

            if (tr.currentSprite == 0)
            {
                tr.gameObject.name = Utils.NAME_TILE_END_FLAG;
                tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
                tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }


    }
    
    // Update is called once per frame
    void Update()
    {
    }

    public void clickDown()
    {
        Debug.Log("clickDown");
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
    
    
    void onTriggerEnterEvent( Collider2D col )
    {
        Debug.Log( "My onTriggerEnterEvent: " + col.gameObject.name );

        if (col.gameObject.name == Utils.NAME_TILE_END_FLAG)
        {
            Debug.Log("YOU WON");
            gameText.text = "YOU WIN";
            gameText.enabled = true;

            StartCoroutine(ReturnToMain());
        }
    }
    
    
    void onTriggerExitEvent( Collider2D col )
    {
        Debug.Log("My onTriggerExitEvent: " + col.gameObject.name );
    }

    IEnumerator ReturnToMain()
    {
        yield return new WaitForSeconds(1.5f);
        Application.LoadLevel("Main");
    }

    public void LoadScene(string scene)
    {
        Application.LoadLevel(scene);
    }
}
