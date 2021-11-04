using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{


    public TextMeshProUGUI masterText;
    public Slider masterSlider;

    public TextMeshProUGUI musicText;
    public Slider musicSlider;

    public TextMeshProUGUI soundText;
    public Slider soundSlider;

    void Update()
    {
        VolumeSliders();

    }

    void VolumeSliders()
    {
        masterText.text = "Sound Volume: " + masterSlider.value + "%";

        musicText.text = "Music Volume: " + musicSlider.value + "%";

        
    }




}
