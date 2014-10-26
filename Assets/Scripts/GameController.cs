using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.IO;

public class GameController : MonoBehaviour
{
    private static float DEFAULT_TILE_SIZE = 1.125731f;
    private static float DEFAULT_TILE_HEIGHT = 800;
    public static ParseController.MapEntity mapToLoad = null;
    public static int sceneToBack = -1;

    public TileRenderer tileRenderer;
    public GameObject mapRoot;
    public GameObject player;
    public Text gameText;
    public UnityEngine.UI.Button b;
    private CharacterController2D controller;

    public UnityEngine.UI.Text diesText;

    public static int numDies = 0;
    public static int lastLevel = 0;
    public bool heroIsDead = false;

    private TileRenderer[] _tiles;
    public TileRenderer[] tiles
    {
        get
        {
            return _tiles;
        }
    }

    private bool finished;

    // Use this for initialization
    void Start()
    {
        finished = false;
        GridRendering rendering = Camera.main.GetComponent<GridRendering> ();
        Debug.Log("TileSize:" + rendering.tileSize);
        Debug.Log("LocalScale:" + player.transform.localScale);
        player.transform.localScale += new Vector3(rendering.tileSize / DEFAULT_TILE_SIZE - 1, rendering.tileSize / DEFAULT_TILE_SIZE -1);
        Debug.Log("LocalScale:" + player.transform.localScale);
        NonPhysicsPlayerTester nppt = player.GetComponent<NonPhysicsPlayerTester>();
        Debug.Log("jump:" + nppt.jumpHeight);
        nppt.jumpHeight += (rendering.tileSize / DEFAULT_TILE_SIZE);
        nppt.gravity += (rendering.tileSize / DEFAULT_TILE_SIZE);
        Debug.Log("jump:" + nppt.jumpHeight);

        /*
        GridRendering rendering = Camera.main.GetComponent<GridRendering> ();
        player.transform.localScale += new Vector3(rendering.tileSize / DEFAULT_TILE_SIZE - 1, rendering.tileSize / DEFAULT_TILE_SIZE -1);


        Resolution res = Screen.currentResolution;
        float scale = res.height / DEFAULT_TILE_HEIGHT;
        Debug.Log("h:" + res.height);
        Debug.Log("scale:" + scale);
        NonPhysicsPlayerTester nppt = player.GetComponent<NonPhysicsPlayerTester>();
        */



		GameController.lastLevel = Application.loadedLevel;
		this.diesText.text = "Times Dead x " + GameController.numDies;

		this.heroIsDead = false;


        controller = player.GetComponent<CharacterController2D>();
        controller.onTriggerEnterEvent += onTriggerEnterEvent;
        controller.onTriggerExitEvent += onTriggerExitEvent;

        _tiles = new TileRenderer[GridRendering.COLS * GridRendering.ROWS];

        
        int xy;
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
            
            
            if (tr.currentSprite == 6 || tr.currentSprite == 15 || tr.currentSprite == 16)
            {
                tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
                tr.name = Utils.NAME_ENEMY_FOLLOWER;
                tr.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            
            
            if (tr.currentSprite == 7 || tr.currentSprite == 8)
            {
                tr.transform.localScale = new Vector3(tr.transform.localScale.x * 0.9f, tr.transform.localScale.y * 0.9f, tr.transform.localScale.z);
                tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
                tr.name = Utils.NAME_ENEMY_FOLLOWER;
                tr.GetComponent<BoxCollider2D>().isTrigger = true;
                tr.GetComponent<SpriteRenderer>().sortingOrder = 1;


                BlockMover mover = tr.gameObject.AddComponent<BlockMover>() as BlockMover;
                mover.oscilation = (tr.currentSprite == 7 ? new Vector3(3, 0) : new Vector3(0, 3) );
            }

            if (tr.currentSprite == 10 || tr.currentSprite == 11)
            {
                BlockMover mover = tr.gameObject.AddComponent<BlockMover>() as BlockMover;
                mover.oscilation = (tr.currentSprite == 10 ? new Vector3(3, 0) : new Vector3(0, 3) );
            }
            
            if (tr.currentSprite == 9)
            {
                BlockSpring spring = tr.gameObject.AddComponent<BlockSpring>() as BlockSpring;
                spring.player = player;
            }
            
            if (tr.currentSprite == 13)
            {
                tr.gameObject.name = Utils.NAME_COIN;
                tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
                tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }

            if (tr.currentSprite == 5)
            {
                tr.gameObject.name = Utils.NAME_TILE_END_FLAG;
                tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
                tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            
            xy = ((int)tr.tile.y) * GridRendering.COLS + ((int)tr.tile.x);
            tiles[xy] = tr;
        }

        SetupLevel();
    }

    private void SetupLevel()
    {
        TileRenderer tr;
        
        /*
        //Create end flag
        tr = (TileRenderer) Instantiate (tileRenderer);
        tr.tile = new Vector3 (GridRendering.COLS - 1, 1);
        tr.currentSprite = 5;
        tr.transform.parent = mapRoot.transform;
        tr.gameObject.name = Utils.NAME_TILE_END_FLAG;
        tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
        tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

*/
        
        for (int i = 0; i < GridRendering.COLS; i++)
        {
            tr = (TileRenderer) Instantiate (tileRenderer);
            tr.tile = new Vector3 (i,-1);
            tr.currentSprite = 0;
            tr.transform.parent = mapRoot.transform;
            tr.gameObject.name = Utils.NAME_TILE_DEAD;
            tr.gameObject.layer = (int)Utils.LAYERS.Triggers;
            tr.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;

            
            /*
            tr = (TileRenderer) Instantiate (tileRenderer);
            tr.tile = new Vector3 (i,GridRendering.ROWS);
            tr.currentSprite = 0;
            tr.transform.parent = mapRoot.transform;
            */
        }
        
        for (int i = 0; i < GridRendering.ROWS + 5; i++)
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

        if (name == Utils.NAME_TILE_DEAD || name == Utils.NAME_ENEMY_FOLLOWER ||
            name == Utils.NAME_ENEMY_FIREBALL /*|| name == Utils.NAME_ENEMY_KOOPA*/)
        {
            Debug.Log("YOU DIE");
            gameText.text = "YOU DIE";
            gameText.color = new Color(219, 24, 24, 255);
            gameText.enabled = true;
            
            StartCoroutine(YouDie());
        }

        if (name == Utils.NAME_COIN)
        {
            Destroy(col.gameObject);
        }
    }
    
    void onTriggerExitEvent(Collider2D col)
    {
        Debug.Log("My onTriggerExitEvent: " + col.gameObject.name);
    }
    
    IEnumerator YouWin()
    {
        if (finished)
            yield return null;
        finished = true;

        ParseController.CompleteLevel(mapToLoad);
        yield return new WaitForSeconds(1.5f);
        LoadScene("Main");
    }
    
    IEnumerator YouDie()
    {
        if (finished)
            yield return null;
        finished = true;

		this.heroIsDead = true;
		GameController.numDies = GameController.numDies + 1;
	
        yield return new WaitForSeconds(1.5f);
        Application.LoadLevel(Application.loadedLevel);



    }

    public void LoadScene(string scene)
    {
        if (GameController.sceneToBack >= 0)
        {
            Application.LoadLevel(GameController.sceneToBack);
            GameController.sceneToBack = -1;
        } else
        {
            Application.LoadLevel(scene);
        }
    }
}
