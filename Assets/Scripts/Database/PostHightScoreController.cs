using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class PostHightScoreController : MonoBehaviour
{
    [SerializeField] private string addScoreURL = "http://localhost/HighScore/addscore.php?";

    [SerializeField] private TMP_InputField nameTextInput;

    private string secretKey = "mySecretKey";
    private int _score;

    public void SendScoreBtn(){
        _score = PlayerPrefs.GetInt("bestScore");
        string name = nameTextInput.text;
        StartCoroutine(PostScores(name, _score));
    }


    IEnumerator PostScores(string name, int score){
        string hash = Md5Sum(name + score + secretKey);


        WWWForm form = new WWWForm();
        form.AddField("namePost", name);
        form.AddField("scorePost", score);
        form.AddField("hashPost", hash);

        UnityWebRequest www = UnityWebRequest.Post(addScoreURL, form);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }

        this.gameObject.SetActive(false );
    }

    public string Md5Sum(string strToEncrypt){
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);
        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);
        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";
        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }
        return hashString.PadLeft(32, '0');
    }
}
