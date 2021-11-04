using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicScript : MonoBehaviour
{
    public AudioClip[] music;
    public AudioSource audioSource;
    private int randomSong;
    private bool muted = false;

    // Start is called before the first frame update
    void Start()
    {
        randomSong = Random.Range(0, 3);
        audioSource.PlayOneShot(music[randomSong], 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            muted = !muted;

            if (muted)
            {
                AudioListener.volume = 0f;
            }
            else
            {
                AudioListener.volume = 1f;
            }
        }
    }
}
