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

    public void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(volumeParameter, volumeSlider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, volumeSlider.value);
    }

    private void HandleSlider(float value)
    {
        mixer.SetFloat(volumeParameter, MathF.Log10(value) * multiplier);
    }
}
