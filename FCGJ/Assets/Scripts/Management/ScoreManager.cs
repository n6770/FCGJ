using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int highScore;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("scoremanager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void SaveScore(int i)
    {
        highScore = i;
    }

}
