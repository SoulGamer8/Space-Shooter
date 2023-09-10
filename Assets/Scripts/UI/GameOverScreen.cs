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
        Color myColor = _text.color;
        Color newColor = new Color(1,1,1,0);
        while (true)
        {
            _text.color = newColor;
            yield return new WaitForSeconds(0.5f);
            _text.color = myColor;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
