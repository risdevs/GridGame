using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Parse;
using System.Threading.Tasks;

public class RankingController : MonoBehaviour
{

    public UnityEngine.UI.InputField NameInput;
    public Canvas mainCanvas;
    public UnityEngine.UI.Text textPrefab;

    public bool multiplayer = false;

    public List<UnityEngine.UI.Text> addedTexts = new List<UnityEngine.UI.Text>();

    // Use this for initialization
    void Start()
    {
        NameInput.text.text = ParseUser.CurrentUser.Username;
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

        int pos = 100;
        string field = multiplayer ? "mpscore" : "spscore";
        foreach (ParseUser u in o.result)
        {
            UnityEngine.UI.Text textName = Instantiate(textPrefab) as UnityEngine.UI.Text;
            textName.text = u.Username;
            textName.transform.parent = mainCanvas.transform;
            textName.transform.localPosition = new Vector3(-30,pos);
            textName.alignment = TextAnchor.MiddleLeft;
            addedTexts.Add(textName);
            UnityEngine.UI.Text textScore = Instantiate(textPrefab) as UnityEngine.UI.Text;
            textScore.text = u [field].ToString();
            textScore.transform.parent = mainCanvas.transform;
            textScore.transform.localPosition = new Vector3(30,pos);
            textScore.alignment = TextAnchor.MiddleRight;
            addedTexts.Add(textScore);
            pos -= 20;

        }
    }

    public void UpdateName()
    {
        ParseUser user = ParseUser.CurrentUser;
        user.Username = NameInput.text.text;
        user.SaveAsync();
    }
    
    // Update is called once per frame
    void Update()
    {
    }
}
