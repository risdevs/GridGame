using UnityEngine;
using System.Collections;
using Gamelogic;
using Gamelogic.Grids;

public class UnitMover : MonoBehaviour
{

    public Main main;
    private Vector3 targetPosition;
    private RectPoint _targetPoint;

    public RectPoint targetPoint
    {
        get
        {
            return _targetPoint;
        }
        set
        {
            _targetPoint = value;
            targetPosition = main.WorldMap [_targetPoint];
        }
    }

    public void Start()
    {
        targetPoint = new RectPoint(0, 0);
        transform.position = targetPosition;
    }
        
    // Update is called once per frame
    void Update()
    {
        if (!hasReachedDestination())
        {
            UpdatePosition();
        }
    }

    void UpdatePosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 2.5f * Time.deltaTime);
    }

    public bool hasReachedDestination()
    {
        return Vector3.Distance(transform.position, targetPosition) < 1e-1f;
    }
}
