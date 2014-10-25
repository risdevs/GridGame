using UnityEngine;
using System.Collections;

public class BlockFollower : MonoBehaviour
{
    public GameObject target;


	private GridRendering gridRendering;
	private Vector3 targetPosition;


    // Use this for initialization
    void Start()
    {
		gridRendering = Camera.main.GetComponent<GridRendering> ();
    }
    
    // Update is called once per frame
    void Update()
    {
		gridRendering.WorldToTile (target.transform.position);
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
