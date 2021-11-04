using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSettings : MonoBehaviour
{

    [SerializeField] Slider muSlider;

    private AudioSource muAso;

    public GameObject musicObject;



    void Start()
    {
        MusicSaver();
    }


    void MusicSaver()
    {
        musicObject = GameObject.FindWithTag("Music");

        muAso = musicObject.GetComponent<AudioSource>();


        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 100);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }

    public void MusicVolume()
    {
        muAso.volume = muSlider.value / 100;
        SaveVolume();
    }


    public void LoadVolume()
    {
        muSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", muSlider.value);
    }


}
