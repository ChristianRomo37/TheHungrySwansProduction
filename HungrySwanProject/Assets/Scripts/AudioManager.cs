using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] string volumeParameter = "MasterVolume";
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider volumeSlider;
    [SerializeField] private float multiplier = 30f;

    public void Awake()
    {
        volumeSlider.onValueChanged.AddListener(HandleSlider);
    }

    private void HandleSlider(float value)
    {
        mixer.SetFloat(volumeParameter, MathF.Log10(value) * multiplier);
    }
}
