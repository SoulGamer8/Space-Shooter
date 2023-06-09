using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeadScreen : MonoBehaviour
{


    [SerializeField] private PlayerHealth _plaertHealth;

    private TextMeshProUGUI  _text;


    private void OnEnable()
    {
        _plaertHealth.PlayerDieEvent += PlayerDead;
    }

    private void OnDisable()
    {
        _plaertHealth.PlayerDieEvent -= PlayerDead;
    }

    private void PlayerDead()
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
