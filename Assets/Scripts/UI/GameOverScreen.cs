using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    private TextMeshProUGUI  _text;

    public void PlayerDead()
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
