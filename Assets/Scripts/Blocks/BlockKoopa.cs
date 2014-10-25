using UnityEngine;
using System.Collections;

public class BlockKoopa : MonoBehaviour
{
    public GameController gameController;

    private TileRenderer tileRenderer;
    private bool left;
    private GridRendering gridRendering;
    public float speed = 0.050f;
    //public float speed = 0.010f;
    private Vector3 direction;

    // Use this for initialization
    void Start()
    {
        gridRendering = Camera.main.GetComponent<GridRendering> ();

        tileRenderer = GetComponent<TileRenderer>();
        Rigidbody2D rigidBody = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rigidBody.gravityScale = 0;
        name = Utils.NAME_ENEMY_KOOPA;
        gameObject.layer = (int)Utils.LAYERS.Triggers;
        GetComponent<BoxCollider2D>().isTrigger = true;

        UpdateDirection();
    }

    void UpdateDirection()
    {
        if (left)
        {
            direction = new Vector3(1, 0);
        } else
        {
            direction = new Vector3(-1, 0);
        }
    }
    
    void Update()
       {
        if (HasCollision())
        {
            left = !left;
            UpdateDirection();
        } else
        {
            transform.position = transform.position + direction * speed;
            tileRenderer.tile = gridRendering.WorldToTile(transform.position);
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {        Debug.Log("Koopa CollisionEnter!" + collision.collider.name);

    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Koopa Collision!" + collision.collider.name);
    }

    public bool HasCollision()
    {
        Vector3 tile = gridRendering.WorldToTile(transform.position);

        int xy = ((int)tile.y) * GridRendering.COLS + ((int)tile.x);

        return gameController.tiles [xy] != null;
    }
}
