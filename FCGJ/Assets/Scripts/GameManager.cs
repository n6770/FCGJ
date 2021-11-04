using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float malfunctionTime = 15f;
    public float malfunctionTimer;
    public Text malfunctionText;
    public Image malfunctionSprite;
    public Animator malfunctionAnimator;
    

    public int score = 0;
    public int highScore = 0;
    public Text scoreText;
    public Text highScoreText;

    public GameObject enemySpawner;
    public bool gameActive = true;

    public int health;
    public Image heart1;
    public Image heart2;
    public Image heart3;

    public int armor;
    public Image armor1;
    public Image armor2;
    public Image armor3;

    public Sprite heartFull;
    public Sprite heartEmpty;
    public Sprite armorFull;
    public Sprite armorEmpty;

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
        //score update
        scoreText.text = "score: " + score;

        //health & armor
        health = playerScript.health;
        armor = playerScript.armor;

        //ui update
        if (health == 3)
        {
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartFull;
        }
        if (health == 2)
        {
            heart1.sprite = heartFull;
            heart2.sprite = heartFull;
            heart3.sprite = heartEmpty;
        }
        if (health == 1)
        {
            heart1.sprite = heartFull;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
        }
        if (health == 0)
        {
            heart1.sprite = heartEmpty;
            heart2.sprite = heartEmpty;
            heart3.sprite = heartEmpty;
        }

        if (armor == 3)
        {
            armor1.sprite = armorFull;
            armor2.sprite = armorFull;
            armor3.sprite = armorFull;
        }
        if (armor == 2)
        {
            armor1.sprite = armorFull;
            armor2.sprite = armorFull;
            armor3.sprite = armorEmpty;
        }
        if (armor == 1)
        {
            armor1.sprite = armorFull;
            armor2.sprite = armorEmpty;
            armor3.sprite = armorEmpty;
        }
        if (armor == 0)
        {
            armor1.sprite = armorEmpty;
            armor2.sprite = armorEmpty;
            armor3.sprite = armorEmpty;
        }

        //malfunctiontimer
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
            malfunctionTime = 10f;
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
            malfunctionTime = 10f;
        }

        malfunctionAnimator.SetTrigger("showpanel");
    }

    void CountScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }
    }

    public void PlayerKilled()
    {
        enemySpawner.SetActive(false);
        Time.timeScale = 0f;
        //retry panel esiin
    }

    void RetryLevel()
    {
        //kesken
        SceneManager.LoadScene(0);
    }

    void MainMenu()
    {

    }
}
