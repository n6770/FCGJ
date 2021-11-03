using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public GameObject explosion;
    public GameObject explosionSpawn;
    public float speed;
    public float knockback;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        speed = speed * Random.Range(0.9f, 1.1f);
        Destroy(gameObject, 2f);
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);  //Shoot
    }

    private void FixedUpdate()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {

            Instantiate(explosion, explosionSpawn.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

}
