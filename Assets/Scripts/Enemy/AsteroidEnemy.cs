using UnityEngine;

public class AsteroidEnemy : Enemy, IMoveable
{
    private void Awake(){
        base.Start();

        transform.rotation = Quaternion.Euler(0,0,Random.Range(0,360)); 
    }

    private void Update(){
        DoMove();
    }

    public void DoMove(){
        transform.position += new Vector3(0, -1, 0) * Time.deltaTime * _speed;
        if (transform.position.y < -5)
            Destroy(this.gameObject);
    }
}
