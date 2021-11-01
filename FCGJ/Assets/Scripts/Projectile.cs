using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float knockback;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(transform.up * speed, ForceMode2D.Impulse);  //Shoot
    }

}
