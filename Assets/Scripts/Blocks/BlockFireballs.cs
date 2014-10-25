using UnityEngine;
using System.Collections;

public class BlockFireballs : MonoBehaviour
{
	public float rotationSpeed = 2;

	private GameObject fireballs;

    // Use this for initialization
    void Start()
	{
		fireballs = (GameObject) Instantiate (GetComponent<TileRenderer> ().fireballRow );

		fireballs.transform.SetParent (gameObject.transform);
		fireballs.transform.localScale = new Vector3 (2, 2);
		//fireballs.transform.position = new Vector3 (0, 0, 0);
		fireballs.transform.localPosition = new Vector3 (0, 0, 0);
    }
    
    // Update is called once per frame
    void Update()
	{
		//fireballs.transform.position = new Vector3 (0, 0, 0);
		fireballs.transform.Rotate (0, 0, rotationSpeed);
    }
}
