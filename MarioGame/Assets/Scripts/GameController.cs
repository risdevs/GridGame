using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Application.LoadLevelAdditive("Level2");
        
        //Application.LoadLevelAdditive("Level" + Random.Range(1,3));
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
}
