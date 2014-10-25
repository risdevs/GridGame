using UnityEngine;
using System.Collections;

public class TileRenderer : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	private Vector3 scale;
	private GridRendering gridRendering;

	public Vector3 tile;
	public Sprite[] sprites;
	public int currentSprite;
	public GameObject fireballRow;

	// Use this for initialization
	void Start ()
	{
		spriteRenderer = (SpriteRenderer)renderer;
        		spriteRenderer.sprite = sprites [currentSprite];

		gridRendering = Camera.main.GetComponent<GridRendering> ();


		Bounds b = spriteRenderer.bounds;
		scale = new Vector3(gridRendering.tileSize / b.size.x, gridRendering.tileSize / b.size.y);

		UpdateSpriteScale ();

		Vector3 v3 = gridRendering.TileToWorld (tile);
		gameObject.transform.position = v3 + (b.size/2 * scale.x); //new Vector3( b.size.x * scale, b.size.y * scale);

    }

	// Update is called once per frame
	void Update ()
	{
		UpdateSpriteScale ();
	}

	private void UpdateSpriteScale()
	{
		transform.localScale = scale;
        (collider2D as BoxCollider2D).size = spriteRenderer.bounds.size;
	}
}
