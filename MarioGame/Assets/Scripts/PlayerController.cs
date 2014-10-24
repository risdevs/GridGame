using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public int horizontalForce, verticalForce;
    private bool grounded;
   
    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        EnforceBounds();

        if (this.rigidbody2D.velocity.y == 0 /*&& transform.position.y < -3.5*/)
        {
            grounded = true;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKey("left"))
        {
            this.rigidbody2D.AddForce(new Vector2(-horizontalForce, 0f));

            if (this.transform.localScale.x > 0)
                this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
        
        if (Input.GetKey("right"))
        {
            this.rigidbody2D.AddForce(new Vector2(horizontalForce, 0f));

            if (this.transform.localScale.x < 0)
                this.transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        }
        
        if (Input.GetKey("up") && grounded)
        {
            this.rigidbody2D.AddForce(new Vector2(0f, verticalForce));
            grounded = false;
        }
    }

    private void EnforceBounds()
    {
        Vector3 newPosition = transform.position; 
        Camera mainCamera = Camera.main;
        Vector3 cameraPosition = mainCamera.transform.position;

        float xDist = mainCamera.aspect * mainCamera.orthographicSize; 
        float xMax = cameraPosition.x + xDist;
        float xMin = cameraPosition.x - xDist;

        float height = GetComponent<SpriteRenderer>().sprite.rect.size.y;
        float yDist = mainCamera.orthographicSize; 
        float yMax = cameraPosition.y + yDist;
        float yMin = cameraPosition.y - yDist;
        
        if (newPosition.x < xMin || newPosition.x > xMax)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, xMin, xMax);
        }

        if (newPosition.y < yMin || newPosition.y > yMax)
        {
            newPosition.y = Mathf.Clamp(newPosition.y, yMin, yMax);
        }

        transform.position = newPosition;
    }
}
