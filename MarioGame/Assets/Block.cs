using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{

    public Sprite[] sprites;
    public int currentSprite;

    // Use this for initialization
    void Start()
    {
    
    }
    
    // Update is called once per frame
    void Update()
    {        GetComponent<SpriteRenderer>().sprite = sprites [currentSprite];
    }
}
