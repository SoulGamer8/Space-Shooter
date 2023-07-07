using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject _text;

    private bool connected = false;

    void Awake()
    {
        StartCoroutine(CheckForControllers());
    }


    IEnumerator CheckForControllers()
    {
        while (true)
        {   
            var controllers = Input.GetJoystickNames();

            if (!connected && controllers.Length > 0)
            {
                connected = true;
                
                Debug.Log("Connected");

            }
            else if (connected && controllers.Length == 0)
            {
                connected = false;
                Debug.Log("Disconnected");
            }
            _text.SetActive(connected);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerable text()
    {
        yield return null;
    }
}
