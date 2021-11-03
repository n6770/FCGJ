using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float malfunctionTime = 15f;
    public float malfunctionTimer;
    public Text malfunctionText;
    public Image malfunctionSprite;
    public Animator malfunctionAnimator;

    public Sprite gravitySprite;
    public Sprite movementSprite;
    public Sprite dashSprite;
    public Sprite weaponsSprite;

    public PlayerScript playerScript;
    private int lastRandom = -1;


    // Start is called before the first frame update
    void Start()
    {
        malfunctionTimer = malfunctionTime;
        playerScript = FindObjectOfType<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (malfunctionTimer > 0f)
        {
            malfunctionTimer -= Time.deltaTime;
        }
        else
        {
            malfunctionTimer = malfunctionTime;
            NewMalfunction();

        }
    }

    void NewMalfunction()
    {
        int random = Random.Range(0, 4);
        if (random == lastRandom)
        {
            NewMalfunction();
            return;
        }

        lastRandom = random;


        if (random == 0)
        {
            //gravity
            playerScript.gravity = false;
            playerScript.movement = true;
            playerScript.dodge = true;
            playerScript.shooting = true;
            malfunctionSprite.sprite = gravitySprite;
            malfunctionText.text = "gravity disabled";
        }
        else if (random == 1)
        {
            //movement
            playerScript.gravity = true;
            playerScript.movement = false;
            playerScript.dodge = true;
            playerScript.shooting = true;
            malfunctionSprite.sprite = movementSprite;
            malfunctionText.text = "movement disabled";
        }
        else if (random == 2)
        {
            //dash
            playerScript.gravity = true;
            playerScript.movement = true;
            playerScript.dodge = false;
            playerScript.shooting = true;
            malfunctionSprite.sprite = dashSprite;
            malfunctionText.text = "dash disabled";
        }
        else if (random == 3)
        {
            //weapons
            playerScript.gravity = true;
            playerScript.movement = true;
            playerScript.dodge = true;
            playerScript.shooting = false;
            malfunctionSprite.sprite = weaponsSprite;
            malfunctionText.text = "weapons disabled";
        }

        malfunctionAnimator.SetTrigger("showpanel");
    }
}
