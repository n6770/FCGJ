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
    public GameObject pauseText;
    public bool paused = false;

    public Animator retryPanel;
    public GameObject inGameUI;
    public SoundManager soundManager;
    public ScoreManager scoreManager;
    

    public int score = 0;
    public Text scoreText;
    public Text endScore;

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
        soundManager = FindObjectOfType<SoundManager>();
        malfunctionTimer = malfunctionTime;
        playerScript = FindObjectOfType<PlayerScript>();
        scoreManager = FindObjectOfType<ScoreManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //score update
        scoreText.text = "score: " + score;

        //health & armor
        health = playerScript.health;
        armor = playerScript.armor;


        //pause
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }

        //quit
        if (paused && Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

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
        soundManager.PlayFX(1, 1f);
        int random = Random.Range(0, 9);
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
            playerScript.multishot = true;
            playerScript.firingSpeed = 0.18f;
            playerScript.movementSpeed = 5f;
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
            playerScript.multishot = true;
            playerScript.firingSpeed = 0.18f;
            playerScript.movementSpeed = 5f;
            malfunctionSprite.sprite = movementSprite;
            malfunctionText.text = "movement disabled";
            malfunctionTimer = 5f;
        }
        else if (random == 2)
        {
            //dash
            playerScript.gravity = true;
            playerScript.movement = true;
            playerScript.dodge = false;
            playerScript.shooting = true;
            playerScript.multishot = true;
            playerScript.firingSpeed = 0.18f;
            playerScript.movementSpeed = 5f;
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
            playerScript.multishot = true;
            playerScript.firingSpeed = 0.18f; 
            playerScript.movementSpeed = 5f;
            malfunctionSprite.sprite = weaponsSprite;
            malfunctionText.text = "weapons disabled";
            malfunctionTimer = 5f;
        }
        else if (random == 4)
        {
            //multishot
            playerScript.gravity = true;
            playerScript.movement = true;
            playerScript.dodge = true;
            playerScript.shooting = true;
            playerScript.multishot = false;
            playerScript.firingSpeed = 0.18f;
            playerScript.movementSpeed = 5f;
            malfunctionSprite.sprite = weaponsSprite;
            malfunctionText.text = "multishot gained";
            malfunctionTimer = 10f;
        }
        else if (random == 5)
        {
            //fire rate up
            playerScript.gravity = true;
            playerScript.movement = true;
            playerScript.dodge = true;
            playerScript.shooting = true;
            playerScript.multishot = true;
            playerScript.firingSpeed = 0.1f;
            playerScript.movementSpeed = 5f;
            malfunctionSprite.sprite = weaponsSprite;
            malfunctionText.text = "fire rate up";
            malfunctionTimer = 10f;
        }
        else if (random == 6)
        {
            //fire rate down
            playerScript.gravity = true;
            playerScript.movement = true;
            playerScript.dodge = true;
            playerScript.shooting = true;
            playerScript.multishot = true;
            playerScript.firingSpeed = 0.3f;
            playerScript.movementSpeed = 5f;
            malfunctionSprite.sprite = weaponsSprite;
            malfunctionText.text = "fire rate down";
            malfunctionTimer = 10f;
        }
        else if (random == 7)
        {
            //speed down
            playerScript.gravity = true;
            playerScript.movement = true;
            playerScript.dodge = true;
            playerScript.shooting = true;
            playerScript.multishot = true;
            playerScript.firingSpeed = 0.18f;
            playerScript.movementSpeed = 3f;
            malfunctionSprite.sprite = weaponsSprite;
            malfunctionText.text = "speed down";
            malfunctionTimer = 10f;
        }
        else if (random == 8)
        {
            //speed up
            playerScript.gravity = true;
            playerScript.movement = true;
            playerScript.dodge = true;
            playerScript.shooting = true;
            playerScript.multishot = true;
            playerScript.firingSpeed = 0.18f;
            playerScript.movementSpeed = 8f;
            malfunctionSprite.sprite = weaponsSprite;
            malfunctionText.text = "speed up";
            malfunctionTimer = 10f;
        }

        malfunctionAnimator.SetTrigger("showpanel");
    }

    void CountScore()
    {
        if (score > PlayerPrefs.GetInt("highscore"))
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void PlayerKilled()
    {
        CountScore();
        soundManager.PlayFX(4, 1f);
        endScore.text = "final score: " + score + "\n high score: " + PlayerPrefs.GetInt("highscore");
        inGameUI.SetActive(false);
        retryPanel.SetTrigger("next");
        Time.timeScale = 0f;
        enemySpawner.SetActive(false);
        
        //retry panel esiin
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void TogglePause()
    {
        paused = !paused;
        if (paused)
        {
            pauseText.SetActive(true);
            Time.timeScale = 0f;
        }
        if (!paused)
        {
            pauseText.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
