using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.volume = volumeSlider.value;
    }

    public void OnVolumeSliderChanged()
    {
        audioSource.volume = volumeSlider.value;
    }
}
