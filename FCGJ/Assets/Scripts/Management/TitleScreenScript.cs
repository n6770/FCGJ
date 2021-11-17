using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreenScript : MonoBehaviour
{
    public Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = "High score: " + PlayerPrefs.GetInt("highscore");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }
}
