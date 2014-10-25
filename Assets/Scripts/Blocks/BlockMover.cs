using UnityEngine;
using System.Collections;

public class BlockMover : MonoBehaviour
{

	public Vector3 oscilation;
    public float speed = 0.040f;

    private Vector3 targetPosition;
    private Vector3 direction;
	private bool sum;


    // Use this for initialization
    void Start()
    {
		sum = true;
		targetPosition = transform.position + oscilation;
        direction = Vector3.Normalize(oscilation);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (hasReachedDestination())
        {
            UpdateDestination();
        } 

        Debug.Log("Mover position:" + transform.position + " Target position:" + targetPosition);

        transform.position = transform.position + direction * speed;
    }

    public bool hasReachedDestination()
    {
        return Vector3.Distance(transform.position, targetPosition) < 1;
    }

    void UpdateDestination()
    {
        if (sum)
        {
            targetPosition = transform.position - 2 * oscilation;
        } else
        {
            targetPosition = transform.position - 2 * oscilation;
        }
        sum = !sum;
        direction.x *= -1;
        direction.y *= -1;
    }
}
