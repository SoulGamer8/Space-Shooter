using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _text;

    private bool connected = false;
    private Coroutine _textBlink;


    void Awake()
    {
        StartCoroutine(CheckForControllers());
    }


    IEnumerator CheckForControllers()
    {
        while (true)
        {
            Debug.Log("Test");
            var controllers = Input.GetJoystickNames();

            if (!connected && controllers.Length > 0)
            {
                connected = true;
                _textBlink = StartCoroutine(TextBlink());
                Debug.Log("Connected");

            }
            else if (connected && controllers.Length == 0)
            {
                connected = false;
                StopCoroutine(_textBlink);
                Debug.Log("Disconnected");
            }
            _text.SetActive(connected);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator TextBlink()
    {
        TextMeshProUGUI text = _text.GetComponent<TextMeshProUGUI>();
        while (true)
        {
            text.text = "Press any key to add  Player2";
            yield return new WaitForSeconds(0.5f);
            text.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
