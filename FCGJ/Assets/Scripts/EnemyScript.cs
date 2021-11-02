using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //enemy variables
    public int health;
    public float speed;

    public float knockbackForce;
    public int knockbackFrames;

    public GameObject player;
    public PlayerScript playerScript;
    public Rigidbody2D playerRb;
    public Rigidbody2D enemyRb;


    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
        playerRb = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            --playerScript.health;
            playerScript.enemyKnockbackFrames = knockbackFrames;
            playerScript.knockbackForce = knockbackForce;
            playerScript.knockBackPos = transform.position;
            playerScript.enemyKnockback = true;
        }
    }
}
