using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rbPlayer;
    public float horSpeed;
    public float verSpeed;
    public float speedMultiplier;


    // Start is called before the first frame update
    void Start()
    {
        rbPlayer = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        horSpeed = rbPlayer.velocity.x * speedMultiplier;
        verSpeed = rbPlayer.velocity.y * speedMultiplier;
        transform.Translate(new Vector3(-horSpeed, -verSpeed, 0f));
    }
}
