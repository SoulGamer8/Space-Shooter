using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;


public class HighScoreControl : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameResultText;
    [SerializeField] private TextMeshProUGUI scoreResultText;

    private string highScoreURL ="http://localhost/HighScore/display.php";

    private void Start(){
        GetScoreBtn();
    }

    private void GetScoreBtn(){
        nameResultText.text = "Player: \n \n";
        scoreResultText.text = "Score: \n \n";
        StartCoroutine(GetScores());
    }
   
    IEnumerator GetScores(){
        UnityWebRequest hs_get = UnityWebRequest.Get(highScoreURL);
        yield return hs_get.SendWebRequest();
        if (hs_get.error != null)
            Debug.Log("There was an error getting the high score: "
                    + hs_get.error);
        else
        {
            string dataText = hs_get.downloadHandler.text;
            MatchCollection mc = Regex.Matches(dataText, @"_");
            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                for (int i = 0; i < mc.Count; i++)
                {
                    if (i % 2 == 0)
                        nameResultText.text +=
                                            splitData[i];
                    else
                        scoreResultText.text +=
                                            splitData[i];
                }
            }
        }
    }
}
