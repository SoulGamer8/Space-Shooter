
using System.Collections;
using UnityEngine;

public class GameControllers : MonoBehaviour
{
    private bool _isControllerConnected = false;

    IEnumerator CheckForControllers(){
        while (true)
        {
            string[] controllers = Input.GetJoystickNames();

            if (!_isControllerConnected && controllers.Length > 0)
            {
                _isControllerConnected = true;
                Debug.Log("Connected");

            }
            else if (_isControllerConnected && controllers.Length == 0)
            {
                _isControllerConnected = false;
                Debug.Log("Disconnected");
            }

            yield return new WaitForSeconds(1f);
        }
    }

    void Awake()
    {
        StartCoroutine(CheckForControllers());
    }

}
