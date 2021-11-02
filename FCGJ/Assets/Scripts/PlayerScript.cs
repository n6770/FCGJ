using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //player variables
    public int health;
    public float armor;
    public float invincibleTime; //time to ignore damage after hit
    public float movementSpeed;
    public float hor, ver;
    public Vector2 mousePos;
    public GameObject gunPos;
    public GameObject projectile;
    public Rigidbody2D rb;
    
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

        if (shooting)
        {
            if (Input.GetButtonDown("Fire1"))   //shooting
            {
                GameObject spawnedProjectile = Instantiate(projectile, gunPos.transform.position, gunPos.transform.rotation);
                if (!gravity)
                {
                    knockbackTriggered = true;
                    knockbackForce = spawnedProjectile.GetComponent<Projectile>().knockback;
                    knockBackPos = spawnedProjectile.transform.position;
                }
            }
        }

        //dodge
        if (dodge)
        {
            if (Input.GetButtonDown("Jump") && dodgeCooldownTime == 0 && (hor != 0f || ver != 0f))
            {
                rb.velocity = new Vector2(0f, 0f);
                dodged = true;
                dodgeCurFrame = dodgeIFrames;
                dodgeCooldownTime = dodgeCooldown;
            }
        }
    }

    void FixedUpdate()
    {

        //rotate player towards mouse
        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;


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
            --dodgeCurFrame;
            rb.AddForce(new Vector2(hor, ver) * dodgeRange, ForceMode2D.Impulse);

            if (dodgeCurFrame == 0)
            {
                dodged = false;
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
            
            --enemyKnockbackFrames;
            rb.AddForce((transform.position - knockBackPos) * knockbackForce, ForceMode2D.Impulse);

            if (enemyKnockbackFrames == 0)
            {
                enemyKnockback = false;
            }
        }
    }
}
