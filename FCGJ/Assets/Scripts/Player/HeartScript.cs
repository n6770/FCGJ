using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public SoundManager soundManager;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<PlayerScript>();
        soundManager = FindObjectOfType<SoundManager>();
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (playerScript.health < 3 )
            {
                soundManager.PlayFX(5, 1f);
                playerScript.health++;
                Destroy(gameObject);
            }
        }
    }
}
