﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SinglePlayerMenu : MonoBehaviour
{

    public UnityEngine.UI.Button level1;
    public UnityEngine.UI.Button level2;
    public UnityEngine.UI.Button level3;
    public UnityEngine.UI.Button buttonPrev;
    public UnityEngine.UI.Button buttonNext;
    public UnityEngine.UI.Button buttonGoBack;
    public int pageCounter;
    public List<ParseController.MapEntity> levels = new List<ParseController.MapEntity>();

    // Use this for initialization
    void Start()
    {
        //UnityEngine.UI.Text a = level1.GetComponentInChildren<UnityEngine.UI.Text> ();
        //a.text = "Level 4";

        levels.Add(new ParseController.MapEntity(new int[,]
            {{1,2,1},{1,6,2}}
        ));

        levels.Add(new ParseController.MapEntity(new int[,]
            {{1,2,1},{1,2,2}}
        ));

        levels.Add(new ParseController.MapEntity(new int[,]
            {{1,2,1},{1,2,2}}
        ));

        pageCounter = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }

    public void ClickBack()
    {
        Application.LoadLevel("Main");
    }

    public void ClickLevel(int level)
    {
        //GameController.LevelToLoad = level;
        int realLevel = level + pageCounter * 3;

        GameController.mapToLoad = levels [0];

        //HORI TO DO -> FALTA CARGAR AQUI EL NIVEL QUE REALMENTE TOCA.
        Application.LoadLevel("Game");
    }

    public void goNext()
    {
        pageCounter = pageCounter + 1;
        
        updateButtons();
        
    }

    public void goPrev()
    {
        pageCounter = pageCounter - 1;
        if (pageCounter < 0)
            pageCounter = 0;

        updateButtons();
    }

    public void updateButtons()
    {

        UnityEngine.UI.Text textLev1 = level1.GetComponentInChildren<UnityEngine.UI.Text>();
        UnityEngine.UI.Text textLev2 = level2.GetComponentInChildren<UnityEngine.UI.Text>();
        UnityEngine.UI.Text textLev3 = level3.GetComponentInChildren<UnityEngine.UI.Text>();
        
        textLev1.text = "LEVEL " + ((pageCounter * 3) + 1);
        textLev2.text = "LEVEL " + ((pageCounter * 3) + 2);
        textLev3.text = "LEVEL " + ((pageCounter * 3) + 3);
        
    }
}
