using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public class RankingController : MonoBehaviour
{

    public Canvas mainCanvas;
    public UnityEngine.UI.Text textPrefab;

    public bool multiplayer = false;

    public List<UnityEngine.UI.Text> addedTexts = new List<UnityEngine.UI.Text>();

    public UnityEngine.UI.Text textUsers;
    public UnityEngine.UI.Text textScores;

    // Use this for initialization
    void Start()
    {
        Debug.Log("ranking start");
        LoadRanking(false);
    }

    public void LoadRanking(bool multiplayer) {
        foreach (UnityEngine.UI.Text text in addedTexts)
        {
            Destroy(text);
        }
        addedTexts = new List<UnityEngine.UI.Text>();
        this.multiplayer = multiplayer;
        StartCoroutine("SetRanking");
    }


    IEnumerator SetRanking()
    {
        Debug.Log("SetRanking");
        var o = new ParseController.GetRankingOperation();
        o.run(multiplayer);
        while (!o.IsCompleted)
            yield return null;
        Debug.Log("load ranking complete");

        int pos = 150;
        string field = multiplayer ? "mpscore" : "spscore";
        int rank = 1;
        string users = "";
        string scores = "";
        foreach (ParseUser u in o.result)
        {
            users += "" + rank + " - " + (u.ObjectId == ParseUser.CurrentUser.ObjectId ? " ---> " : "") + u.Username + "\n";
            scores += u [field].ToString() + "\n";
            rank++;

        }
        textUsers.text = users;
        textScores.text = scores;
    }

    
    // Update is called once per frame
    void Update()
    {
    }
}
