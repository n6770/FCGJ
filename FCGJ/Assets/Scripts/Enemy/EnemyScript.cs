using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyScript : MonoBehaviour
{
    //enemy variables
    public float health;
    public float speed;
    public int score;

    public GameManager gameManager;
    public GameObject gunPos;
    public Rigidbody2D rbGunPos;
    public float shootRange;
    public bool inRange = false;
    public float reloadTime;
    public float timeSinceShot;
    public bool readyToShoot;
    public GameObject projectile;
    public Animator animator;
    public GameObject heart;
    public GameObject deathParticles;
    public SoundManager soundManager;

    public float knockbackForce;
    public int knockbackFrames;

    public GameObject player;
    public PlayerScript playerScript;
    public Rigidbody2D playerRb;
    public Rigidbody2D enemyRb;

    //Pathfinding
    public AIPath aiPath;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerScript>();
        playerRb = player.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("CheckRange", 1f, 0.5f);
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector2 lookDir = playerRb.position - enemyRb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rbGunPos.rotation = angle;
        rbGunPos.position = transform.position;

        animator.SetFloat("hor", enemyRb.velocity.x);
        animator.SetFloat("ver", enemyRb.velocity.y);

        if (timeSinceShot > 0f)
        {
            timeSinceShot = timeSinceShot - Time.deltaTime;
        }

        else if (timeSinceShot <= 0f)
        {
            readyToShoot = true;
        }

        if (readyToShoot && inRange)
        {
            Shoot();
        }

        if (health <= 0)
        {
            Kill();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            playerScript.TakeDamage();
            playerScript.enemyKnockbackFrames = knockbackFrames;
            playerScript.knockbackForce = knockbackForce;
            playerScript.knockBackPos = transform.position;
            playerScript.enemyKnockback = true;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            Vector3 knockBackPos = collision.transform.position;
            
            enemyRb.AddForce((transform.position - knockBackPos) * knockbackForce, ForceMode2D.Impulse);

            health -= collision.GetComponent<Projectile>().damage;
        }
        
        if (collision.gameObject.tag == "PlayerProjectileExplosion")
        {
            health -= collision.GetComponent<GrenadeExplosion>().damage;
        }
    }

    void CheckRange()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < shootRange)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }
    }

    void Shoot()
    {
        soundManager.PlayFX(6, 0.2f);
        Instantiate(projectile, gunPos.transform.position, gunPos.transform.rotation);
        timeSinceShot = reloadTime;
        readyToShoot = false;
    }

    void Kill()
    {
        soundManager.PlayFX(3, 0.2f);
        float heartRandom = Random.Range(0f, 1f);
        if (heartRandom < 0.1f)
        {
            Instantiate(heart, transform.position, heart.transform.rotation);
        }

        Instantiate(deathParticles, transform.position, Quaternion.identity);
        gameManager.score += score;
        Destroy(gameObject);
    }
}
