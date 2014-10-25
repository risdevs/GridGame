using UnityEngine;
using System.Collections;

[RequireComponent( typeof( SpriteRenderer ), typeof(BoxCollider2D ) )]
public class BlockFader : MonoBehaviour
{
	public bool hideCollider = false;
	public float smoothFadingFactor = 1;

	private bool hiding;
	private Color transparentColor;
	private Color realColor;
	private SpriteRenderer spriteRenderer;


    // Use this for initialization
    void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		hiding = true;
		realColor = spriteRenderer.color;
		transparentColor = realColor;
		transparentColor.a = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
		spriteRenderer.color = Color.Lerp(spriteRenderer.color, (hiding ? transparentColor : realColor), Time.deltaTime * smoothFadingFactor);

		if (shouldFlip ()) 
		{
			hiding = !hiding;
		}


		if (hideCollider)
		{
			GetComponent<BoxCollider2D> ().enabled = spriteRenderer.color.a > 0.2f;
		}
    }

    public bool shouldFlip()
    {
		float dist = hiding ? spriteRenderer.color.a - transparentColor.a : spriteRenderer.color.a - realColor.a;

		return Mathf.Abs (dist) < 5e-2f;
    }
}
