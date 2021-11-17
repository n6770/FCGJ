using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade: MonoBehaviour
{
    public GameObject explosion;
    public GameObject explosionSpawn;
    public float speed;
    public float knockback;
    public float lifeTime;
    public float damage;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);  //Shoot
    }

    private void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Pickup" 
            && collision.gameObject.tag !="PlayerProjectile" && collision.gameObject.tag != "PlayerProjectileExplosion")
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Instantiate(explosion, explosionSpawn.transform.position, Quaternion.identity);
    }
}
