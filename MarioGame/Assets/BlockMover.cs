using UnityEngine;
using System.Collections;

public class BlockMover : MonoBehaviour
{

    private Vector3 targetPosition;

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!hasReachedDestination())
        {
            UpdatePosition();
        } else
        {
            targetPosition = transform.position + new Vector3(10, 10);
        }
    }

    void UpdatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.5f * Time.deltaTime);
    }

    public bool hasReachedDestination()
    {
        return Vector3.Distance(transform.position, targetPosition) < 1e-1f;
    }
}
