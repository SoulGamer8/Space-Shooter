using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed;
    [TooltipAttribute("0 - Triple Shot\n" + "1 - Speed\n" + "2 - Shield")]
    [SerializeField] private int _powerUpId;
   
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -5)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            switch (_powerUpId)
            {
                case 0:
                    collision.GetComponent<Shoot>().TakePowerUp();
                    break;
                case 1:
                    collision.GetComponent<PlayerMovement>().TakeSpeedPowerUp();
                    break;
                case 2:
                    collision.GetComponent<PlayerHealth>().ActivateShild();
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }
    }


}
