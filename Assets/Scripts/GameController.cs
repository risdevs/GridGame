using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public static int LevelToLoad = 1; 

    // Use this for initialization
    void Start()
    {
        Application.LoadLevelAdditive("Level" + LevelToLoad);
        
        //Application.LoadLevelAdditive("Level" + Random.Range(1,3));
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
}
