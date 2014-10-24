using UnityEngine;
using System.Collections;

public class BlockMover : MonoBehaviour
{

	public Vector3 oscilation;

    private Vector3 targetPosition;

	private bool sum;


    // Use this for initialization
    void Start()
    {
		sum = true;
		targetPosition = transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!hasReachedDestination())
        {
            UpdatePosition();
        } else
        {
			targetPosition = (sum ? transform.position + oscilation : transform.position - oscilation);
			sum = !sum;
        }
    }

    void UpdatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
    }

    public bool hasReachedDestination()
    {
        return Vector3.Distance(transform.position, targetPosition) < 1e-1f;
    }
}
