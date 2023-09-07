using System.Numerics;
using System.Collections;
using UnityEngine;


public abstract class Enemy : MonoBehaviour, IDamageable, ISpawnChanceWeight
{
    [Header("Enemy Settings")]
    [SerializeField] protected int _health=3;
    [SerializeField] protected float _speed=4;
    [SerializeField] protected int _damage=1;
    [SerializeField] protected int _score = 1;
    [SerializeField] protected int _spawnChanceWeight;

    [Header("Dead")]
    [SerializeField] protected GameObject _explosion;

    
    private SpriteRenderer spriteRenderer;
    private Color myColor;

    public virtual void Start() {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        myColor = spriteRenderer.color;
    }


    public abstract int GetSpawnChanceWeight();

    public virtual void Damage(int damage){
        _health -= damage;
        if(_health < 0)
            Dead();
        StartCoroutine(TakeDamageCoroutine());
    }

    protected virtual void OnTriggerEnter2D(Collider2D collider){
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(collider.tag == "Player")
            damageable.Damege(1);
    }
    
    protected virtual void Dead(){
        Instantiate(_explosion,transform.position);
        Destroy(gameObject);
    }


    protected virtual IEnumerator TakeDamageCoroutine(){
        float timer = 0;

        Color redColor = new Color(1,0,0,1);

        while(timer <0.2f){
            timer += Time.deltaTime;
            float lerpProgress = Mathf.Pow(Mathf.Sin(timer *(Mathf.PI/2)/0.2f),2);
            spriteRenderer.color = Color.Lerp(myColor,redColor,lerpProgress);
            yield return null;
        }

        spriteRenderer.color = myColor;
    }


}
