using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;

public class RankingController : MonoBehaviour
{

    public UnityEngine.UI.InputField NameInput;
    public Canvas mainCanvas;
    public UnityEngine.UI.Text textPrefab;

    // Use this for initialization
    void Start()
    {
        NameInput.text.text = ParseUser.CurrentUser.Username;
        Debug.Log("ranking start");
        StartCoroutine("SetRanking");
    }

    IEnumerator SetRanking()
    {
        Debug.Log("SetRanking");
        var o = new ParseController.GetRankingOperation();
        o.run();
        while (!o.IsCompleted)
            yield return null;
        Debug.Log("load ranking complete");

        int pos = 100;
        foreach (ParseUser u in o.result)
        {
            Debug.Log("CANALETA!!!");
            UnityEngine.UI.Text textName = Instantiate(textPrefab) as UnityEngine.UI.Text;
            textName.text = u.Username;
            textName.transform.parent = mainCanvas.transform;
            textName.transform.localPosition = new Vector3(-30,pos);
            textName.alignment = TextAnchor.MiddleLeft;
            Debug.Log("CANALETA FINISH!!!");
            UnityEngine.UI.Text textScore = Instantiate(textPrefab) as UnityEngine.UI.Text;
            textScore.text = u ["spscore"].ToString();
            textScore.transform.parent = mainCanvas.transform;
            textScore.transform.localPosition = new Vector3(30,pos);
            textScore.alignment = TextAnchor.MiddleRight;
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
