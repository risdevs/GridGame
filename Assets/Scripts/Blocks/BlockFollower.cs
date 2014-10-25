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
		gameObject.layer = (int)Utils.LAYERS.Triggers;
		GetComponent<BoxCollider2D> ().isTrigger = true;
    }
    
    // Update is called once per frame
    void Update()
    {
		//gridRendering.WorldToTile (target.transform.position);
		targetPosition = target.transform.position;
		UpdatePosition ();
    }

    void UpdatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 0.5f);
    }

    public bool hasReachedDestination()
    {
        return Vector3.Distance(transform.position, targetPosition) < 1e-1f;
    }
}
