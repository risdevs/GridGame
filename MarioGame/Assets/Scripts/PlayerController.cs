using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public int horizontalForce, verticalForce;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        handleInput();
	}

    private void handleInput()
    {
        if (Input.GetKey("left"))
        {
            this.rigidbody2D.AddForce(new Vector2(-horizontalForce, 0f));
        }
        
        if (Input.GetKey("right"))
        {
            this.rigidbody2D.AddForce(new Vector2(horizontalForce, 0f));
        }
        
        if (Input.GetKey("up"))
        {
            this.rigidbody2D.AddForce(new Vector2(0f, verticalForce));
        }
    }
}
