using UnityEngine;
using System.Collections;

public class BlockSpring : MonoBehaviour
{
    public GameObject player;
    
    // Use this for initialization
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 dist = player.transform.position - this.transform.position;
        if (Mathf.Abs(dist.x) < 1 && dist.y > 1 && dist.y < 1.5)
        {
            player.GetComponent<NonPhysicsPlayerTester>().spring = true;
        }
          
    }

}
