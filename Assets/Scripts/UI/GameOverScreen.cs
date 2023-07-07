using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    private TextMeshProUGUI  _text;

    private bool _isPlayerDead;


    public void ReloadScene()
    {
        if (_isPlayerDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            _isPlayerDead =false;
        }
    }


    public void PlayerDead()
    {

        StartCoroutine(GameOverText());
        _isPlayerDead = true;
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
