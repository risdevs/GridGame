using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NonPhysicsPlayerTester : MonoBehaviour
{
	// movement config
	public float gravity = -25f;
	public float runSpeed = 8f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster
	public float inAirDamping = 5f;
	public float jumpHeight = 3f;

	[HideInInspector]
	private float normalizedHorizontalSpeed = 0;

	private CharacterController2D _controller;
	private Animator _animator;
	private RaycastHit2D _lastControllerColliderHit;
	private Vector3 _velocity;
  

    
    bool RightPressed = false;
    bool LeftPressed = false;
    bool JumpPressed = false;
    bool RightOver = false;
    bool LeftOver = false;
    bool JumpOver = false;
    
    void Awake()
	{
		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();

		// listen to some events for illustration purposes
		_controller.onControllerCollidedEvent += onControllerCollider;
		_controller.onTriggerEnterEvent += onTriggerEnterEvent;
		_controller.onTriggerExitEvent += onTriggerExitEvent;
	}


	#region Event Listeners

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits cause they arent very interesting
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits if uncommented. it gets noisy so it is commented out for the demo
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void onTriggerEnterEvent( Collider2D col )
	{
		Debug.Log( "onTriggerEnterEvent: " + col.gameObject.name );
	}


	void onTriggerExitEvent( Collider2D col )
	{
		Debug.Log( "onTriggerExitEvent: " + col.gameObject.name );
	}

	#endregion

	GameObject GetHitObjects(Vector3 pos) {
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);
		if (hit.collider != null) {
			return hit.collider.gameObject;
		} else {
			return null;
		}
	}

	List<GameObject> GetHitObjects() {
		List<GameObject> gameObjects = new List<GameObject> ();
		if (Input.touchCount == 0) {
			// MOUSE INPUT
			if (Input.GetMouseButton(0)) {
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (hit.collider != null) {
					gameObjects.Add (hit.collider.gameObject);
				}
			}
		} else {
			// TOUCH INPUT
			for (int i = 0; i < Input.touchCount; i++) {
				Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
				Vector2 touchPos = new Vector2(wp.x, wp.y);
				Collider2D collider2D = Physics2D.OverlapPoint(touchPos);

				if (collider2D != null)
				{
					//your code
					gameObjects.Add(collider2D.gameObject);
				}

			}
		}
		return gameObjects;
	}

	void Start() {
		Input.multiTouchEnabled = true; 
	}

	// the Update loop contains a very simple example of moving the character around and controlling the animation
	void Update()
     {

		if( Input.GetKey( KeyCode.RightArrow )) {
			RightPressed = true;
		}
		if (Input.GetKey( KeyCode.LeftArrow )) {
			LeftPressed = true;
		}
		if (Input.GetKey( KeyCode.UpArrow )) {
			JumpPressed = true;
		}

		List<GameObject> hitObjects = GetHitObjects ();
		
		// grab our current _velocity to use as a base for all calculations
		_velocity = _controller.velocity;

		if( _controller.isGrounded )
			_velocity.y = 0;

		if( RightPressed || RightOver)
		{
			normalizedHorizontalSpeed = 1;
			if( transform.localScale.x < 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else if( LeftPressed || LeftOver )
		{
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Run" ) );
		}
		else
		{
			normalizedHorizontalSpeed = 0;

			if( _controller.isGrounded )
				_animator.Play( Animator.StringToHash( "Idle" ) );
		}


		// we can only jump whilst grounded
		if( _controller.isGrounded && (JumpPressed || JumpOver))
		{
			_velocity.y = Mathf.Sqrt( 2f * jumpHeight * -gravity );
			_animator.Play( Animator.StringToHash( "Jump" ) );
		}


		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		_velocity.x = Mathf.Lerp( _velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor );

		// apply gravity before moving
		_velocity.y += gravity * Time.deltaTime;

		_controller.move( _velocity * Time.deltaTime );
	}


    public void setLeftDown(bool down)
    {
        LeftPressed = down;
    }
    
    
    public void setRightDown(bool down)
    {
        RightPressed = down;
    }
    
    
    public void setJumpDown(bool down)
    {
        JumpPressed = down;
    }

    
    public void setLeftOver(bool down)
    {
        LeftOver = down;
    }
    
    
    public void setRightOver(bool down)
    {
        RightOver = down;
    }
    
    
    public void setJumpOver(bool down)
    {
        JumpOver = down;
    }
}
