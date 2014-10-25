using UnityEngine;
using System.Collections;

public class BlockFollower : MonoBehaviour
{
    public GameObject target;
    private Vector3 targetPosition;
    private Vector3 direction;
    public float speed = 0.040f;


    // Use this for initialization
    void Start()
    {
        gameObject.layer = (int)Utils.LAYERS.Triggers;
        name = Utils.NAME_ENEMY_FOLLOWER;
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        //gridRendering.WorldToTile (target.transform.position);
        targetPosition = target.transform.position;
        direction = Vector3.Normalize(targetPosition - transform.position);

        UpdatePosition();
    }

    void UpdatePosition()
    {
        transform.position = transform.position + direction * speed;
    }

    public bool hasReachedDestination()
    {
        return Vector3.Distance(transform.position, targetPosition) < 1e-1f;
    }
}
