using UnityEngine;

public class AsteroidEnemy : Enemy, IMoveable
{
    [SerializeField] private Sprite[] _randomSprite;
    private Vector3 _targetRandomPosition;
    private void Awake(){
        base.Start();


        GetComponent<SpriteRenderer>().sprite = _randomSprite[Random.Range(0,_randomSprite.Length)];
        transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360)); 
        _targetRandomPosition = new Vector3(Random.Range(-13,13),-8,0);
        
    }

    private void Update(){
        DoMove();
    }

    public void DoMove(){
        transform.position  = Vector3.MoveTowards(transform.position,_targetRandomPosition,Time.deltaTime * _speed);                      
        if (transform.position.y < -6)
            Destroy(this.gameObject);
    }
}
