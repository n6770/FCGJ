using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //player variables
    public int health;
    public int armor;
    public float firingSpeed;
    public float timeSinceLastShot;
    public int invincibleTime; //time to ignore damage after hit
    public int curInvincibleTime;
    public float movementSpeed;
    public float hor, ver;
    public Vector2 mousePos;
    public GameObject gunPos;
    public GameObject projectile;
    public Rigidbody2D rb;
    public Rigidbody2D rbGun;
    public CircleCollider2D circleCollider2D;
    public Animator animator;
    public GameManager gameManager;
        
    //mechanics
    public bool gravity = true;
    public bool movement = true;
    public bool dodge = true;
    public bool shooting = true;
    public bool armored = true;

    //dodge variables
    public bool dodged = false;
    public float dodgeRange;
    public int dodgeCooldown;
    public int dodgeCooldownTime;
    public int dodgeIFrames;
    public int dodgeCurFrame = 0;

    //knockback variables
    public bool knockbackTriggered = false;
    public float knockbackForce;
    public float maxSpeed;
    public Vector3 knockBackPos;
    public bool enemyKnockback = false;
    public int enemyKnockbackFrames;

    
    void Start()
    {

    }

    private void Update()
    {

        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        //clamp to max speed
        if (Mathf.Abs(rb.velocity.x ) > maxSpeed || Mathf.Abs(rb.velocity.y) > maxSpeed)
        {
            Vector3 newSpeed = rb.velocity.normalized;
            newSpeed *= maxSpeed;
            rb.velocity = newSpeed;
        }

        if (firingSpeed > 0f)
        {
            timeSinceLastShot = timeSinceLastShot + Time.deltaTime;
        }

        if (shooting)
        {
            animator.SetBool("gun", true);
            if (Input.GetButton("Fire1") && timeSinceLastShot > firingSpeed)   //shooting
            {
                animator.SetBool("Movement", true);
                Shoot();
            }

            if (Input.GetButtonUp("Fire1"))
            {
                animator.SetBool("Movement", false);
            }
        }
        if (!shooting)
        {
            animator.SetBool("gun", false);
        }

        //dodge
        if (dodge)
        {
            if (Input.GetButtonDown("Jump") && dodgeCooldownTime == 0 && (hor != 0f || ver != 0f))
            {
                rb.velocity = new Vector2(0f, 0f);
                dodged = true;
                animator.SetTrigger("dodge");
                dodgeCurFrame = dodgeIFrames;
                dodgeCooldownTime = dodgeCooldown;
            }
        }
    }

    void FixedUpdate()
    {
        if (curInvincibleTime > 0)
        {
            curInvincibleTime--;
        }


        //rotate player towards mouse
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rbGun.rotation = angle;
        rbGun.position = transform.position;

        animator.SetFloat("hor", lookDir.x);
        animator.SetFloat("ver", lookDir.y);

        if (Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f)
        {
            animator.SetBool("Movement", true);
        }
        else
        {
            animator.SetBool("Movement", false);
        }

        //movement
        if ((movement && !dodged && !enemyKnockback))
        {
            if (gravity)
            {
                rb.velocity = new Vector2(hor * movementSpeed, ver * movementSpeed);
            }
            if (!gravity)
            {
                rb.AddForce(new Vector2(hor * movementSpeed, ver * movementSpeed) * movementSpeed);

            }
        }
        
        if (!movement && !dodged && gravity)    //to stop with gravity enabled without movement after dodge
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, 0.3f); //stop speed
        }

        //dodge
        if (dodged)
        {
            animator.SetFloat("moveHor", rb.velocity.x);
            animator.SetFloat("moveVer", rb.velocity.y);
            --dodgeCurFrame;
            rb.AddForce(new Vector2(hor, ver) * dodgeRange, ForceMode2D.Impulse);
            gameObject.layer = 8;
            if (dodgeCurFrame == 0)
            {
                dodged = false;
                gameObject.layer = 0;
            }
        }
        

        if (dodgeCooldownTime != 0)
        {
            --dodgeCooldownTime;
        }

        //gravity/knockback
        if (knockbackTriggered)
        {
            rb.AddForce((transform.position - knockBackPos) * knockbackForce, ForceMode2D.Impulse);
            knockbackTriggered = false;
        }

        if (enemyKnockback)
        {

            animator.SetFloat("moveHor", rb.velocity.x);
            animator.SetFloat("moveVer", rb.velocity.y);

            --enemyKnockbackFrames;
            rb.AddForce((transform.position - knockBackPos) * knockbackForce, ForceMode2D.Impulse);

            if (enemyKnockbackFrames == 0)
            {
                enemyKnockback = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            animator.SetTrigger("damage");
            knockbackForce = collision.gameObject.GetComponent<EnemyProjectile>().knockback;
            knockBackPos = collision.gameObject.transform.position;
            enemyKnockback = true;
            enemyKnockbackFrames = 8;
            TakeDamage();
        }
    }

    void Shoot()
    {
        timeSinceLastShot = 0f;
        GameObject spawnedProjectile = Instantiate(projectile, gunPos.transform.position, gunPos.transform.rotation);
        if (!gravity)
        {
            knockbackTriggered = true;
            knockbackForce = spawnedProjectile.GetComponent<Projectile>().knockback / 2;
            knockBackPos = spawnedProjectile.transform.position;
        }
    }

    void Kill()
    {
        gameManager.PlayerKilled();
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        if (curInvincibleTime < 0)
        {
            curInvincibleTime = invincibleTime;
        }
        if (curInvincibleTime == 0)
        {
            if (armor != 0)
            {
                --armor;
            }
            if (armor == 0)
            {
                --health;
            }

        }
            if (health == 0)
            {
                Kill();
            }
    }
}
