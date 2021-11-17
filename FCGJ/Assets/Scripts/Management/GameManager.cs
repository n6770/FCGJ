using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float malfunctionTime = 15f;
    public float malfunctionTimer;
    public Text movementMalfunctionText;
    public Text weaponsMalfunctionText;
    public Image malfunctionSprite;
    public Animator malfunctionAnimator;
    public GameObject pauseText;
    public bool paused = false;
    public WeaponsMalfunctionSO[] weaponsMalfunctions;
    public MovementMalfunctionSO[] movementMalfunctions;
    public WeaponSO[] weapons;

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
    private int movementLastRandom = -1;
    private int weaponsLastRandom = -1;


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
            NewMalfunction();

        }
    }

    void NewMalfunction()
    {

        int movementRandom = Random.Range(0, movementMalfunctions.Length);
        int weaponsRandom = Random.Range(0, weaponsMalfunctions.Length);

        if (movementRandom == movementLastRandom || weaponsRandom == weaponsLastRandom)
        {
            NewMalfunction();
            return;
        }
        
        soundManager.PlayFX(1, 1f);
            
        movementLastRandom = movementRandom;
        weaponsLastRandom = weaponsRandom;

        //write new movement values to playerScript
        playerScript.gravity = movementMalfunctions[movementRandom].gravity;
        playerScript.movement = movementMalfunctions[movementRandom].movement;
        playerScript.dodge = movementMalfunctions[movementRandom].dodge;
        playerScript.movementSpeed = movementMalfunctions[movementRandom].movementSpeed;
        movementMalfunctionText.text = movementMalfunctions[movementRandom].malfunctionText;

        //write new weapons values to playerScript
        playerScript.shooting = weaponsMalfunctions[weaponsRandom].shooting;
        playerScript.multishot = weaponsMalfunctions[weaponsRandom].multishot;
        playerScript.firingSpeed = weaponsMalfunctions[weaponsRandom].firingSpeed;
        weaponsMalfunctionText.text = weaponsMalfunctions[weaponsRandom].malfunctionText;

        //malfunction timer reset, lower value is always used
        malfunctionTimer = Mathf.Min(movementMalfunctions[movementRandom].malfunctionTime, 
                               weaponsMalfunctions[weaponsRandom].malfunctionTime);

        //weapon randomizer
        int swapWeaponRandom = Random.Range(0, 10);
        if (swapWeaponRandom <= 10)
        {
            int weaponRandom = Random.Range(0, weapons.Length);
            playerScript.projectile = weapons[weaponRandom].projectile;
            playerScript.firingSpeedMultiplier = weapons[weaponRandom].firingSpeedMultiplier;
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
        //disable enemyScripts, enemyAI, bg parallaxFX and spawner
        enemySpawner.SetActive(false);
        BackgroundParallax bg = FindObjectOfType<BackgroundParallax>();
        bg.enabled = false;
        EnemyScript[] enemies = FindObjectsOfType<EnemyScript>();
        EnemyAI[] enemiesAI = FindObjectsOfType<EnemyAI>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].enabled = false;
            enemiesAI[i].enabled = false;
        }
        
        //activate retrypanel and count scores
        soundManager.PlayFX(4, 1f);
        CountScore();
        endScore.text = "final score: " + score + "\n high score: " + PlayerPrefs.GetInt("highscore");
        inGameUI.SetActive(false);
        retryPanel.SetTrigger("next");
        Time.timeScale = 0f;
        
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
