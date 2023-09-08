using UnityEngine;


[RequireComponent(typeof(Collider2D), typeof(AudioSource))]
public class PowerUp : MonoBehaviour,ISpawnChanceWeight
{

    [SerializeField] private float _speed;
    [SerializeField] private int _spawnChanceWeight;
    private enum TypePowerUp {TripleShot,Speed,Shield,Respawn,Repair,Ammo};
    [SerializeField] private TypePowerUp _powerUpId;

    [Header("Sound")]
    [SerializeField] private AudioClip _soundTakePowerUp;

    public int GetSpawnChanceWeight()
    {
        return _spawnChanceWeight;
    }

    public void ChangeSpawnChanceWeight(int spawnChanceWeight){
        _spawnChanceWeight += spawnChanceWeight;
    }

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
            switch ((int)_powerUpId)
            {
                case 0:
                    collision.GetComponent<Shoot>().TakeTripleShoot();
                    break;
                case 1:
                    collision.GetComponent<PlayerMovement>().TakeSpeedPowerUp();
                    break;
                case 2:
                    collision.GetComponent<PlayerHealth>().ActivateShied();
                    break;
                case 3:
                    collision.GetComponent<PlayerHealth>().RespawnPlayer();
                    break;
                case 4:
                    collision.GetComponent<PlayerHealth>().TakeHeal(); 
                    break;
                case 5:
                    collision.GetComponent<Shoot>().TakeAmmo();
                    break;
                default:
                    break;
            }
           

            AudioSource.PlayClipAtPoint(_soundTakePowerUp,transform.position);

            Destroy(gameObject);
        }
    }

}
