using UnityEngine;
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


        foreach (ParseController.MapTile t in mapToLoad.tiles)
        {
            TileRenderer tr = (TileRenderer)Instantiate(tileRenderer);
            tr.tile = new Vector3(t.x, t.y);
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

        SetupLevel();
    }

    private void SetupLevel()
    {
        TileRenderer tr;
        int xy;
        
        
        //Create end flag
        tr = (TileRenderer) Instantiate (tileRenderer);
        tr.tile = new Vector3 (GridRendering.COLS - 1, 1);
        tr.currentSprite = 5;
        tr.transform.parent = mapRoot.transform;
        tr.gameObject.name = Utils.NAME_TILE_END_FLAG;
        tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
        tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        
        xy = ((int)tr.tile.y) * GridRendering.COLS + ((int)tr.tile.x);

        
        for (int i = 0; i < GridRendering.COLS; i++)
        {
            tr = (TileRenderer) Instantiate (tileRenderer);
            tr.tile = new Vector3 (i,-1);
            tr.currentSprite = 0;
            tr.transform.parent = mapRoot.transform;
            tr.gameObject.name = Utils.NAME_TILE_DEAD;
            tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
            tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            
            
            
            
            tr = (TileRenderer) Instantiate (tileRenderer);
            tr.tile = new Vector3 (i,GridRendering.ROWS);
            tr.currentSprite = 0;
            tr.transform.parent = mapRoot.transform;
        }
        
        for (int i = 0; i < GridRendering.ROWS; i++)
        {
            tr = (TileRenderer) Instantiate (tileRenderer);
            tr.tile = new Vector3 (-1,i);
            tr.currentSprite = 0;
            tr.transform.parent = mapRoot.transform;
            
            
            tr = (TileRenderer) Instantiate (tileRenderer);
            tr.tile = new Vector3 (GridRendering.COLS, i);
            tr.currentSprite = 0;
            tr.transform.parent = mapRoot.transform;
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
            TileRenderer tr = (TileRenderer)Instantiate(tileRenderer);
            tr.tile = new Vector3(i, 0);
            tr.currentSprite = i % 3;
            tr.transform.parent = mapRoot.transform;
        }
    }
    
    void onTriggerEnterEvent(Collider2D col)
    {
        Debug.Log("My onTriggerEnterEvent: " + col.gameObject.name);

        string name = col.gameObject.name;

        if (name == Utils.NAME_TILE_END_FLAG)
        {
            Debug.Log("YOU WON");
            gameText.text = "YOU WIN";
            gameText.color = new Color(24, 219, 24, 255);
            gameText.enabled = true;

            StartCoroutine(YouWin());
        }

        if (name == Utils.NAME_TILE_DEAD || name == Utils.NAME_ENEMY_FOLLOWER || name == Utils.NAME_ENEMY_FIREBALL)
        {
            Debug.Log("YOU DIE");
            gameText.text = "YOU DIE";
            gameText.color = new Color(219, 24, 24, 255);
            gameText.enabled = true;
            
            StartCoroutine(YouDie());
        }
    }
    
    void onTriggerExitEvent(Collider2D col)
    {
        Debug.Log("My onTriggerExitEvent: " + col.gameObject.name);
    }
    
    IEnumerator YouWin()
    {
        ParseController.CompleteSinglePlayerLevel(mapToLoad.Number);
        yield return new WaitForSeconds(1.5f);
        Application.LoadLevel("Main");
    }
    
    IEnumerator YouDie()
    {
        yield return new WaitForSeconds(1.5f);
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoadScene(string scene)
    {
        Application.LoadLevel(scene);
    }
}
