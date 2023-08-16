using System.Collections;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private TextMeshProUGUI  _text;

    public void Start()
    {
        StartCoroutine(GameOverText());
    }


    private IEnumerator GameOverText()
    {
        _text = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        while (true)
        {
            _text.text = "Press \"R\" to restart";
            yield return new WaitForSeconds(0.5f);
            _text.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
