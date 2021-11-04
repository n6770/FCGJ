using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    private bool muted = false;
    public void PlayFX(int sound, float volume)
    {
        audioSource.PlayOneShot(audioClips[sound], volume);
    }

    private void Update()
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
