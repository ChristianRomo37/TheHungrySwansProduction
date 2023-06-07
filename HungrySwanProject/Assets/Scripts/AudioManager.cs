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
    [SerializeField] private Toggle toggle;
    private bool _disableToggleEvent;

    public void Awake()
    {
        volumeSlider.onValueChanged.AddListener(HandleSlider);
        toggle.onValueChanged.AddListener(HandleToggle);
    }

    private void HandleToggle(bool enableSound)
    {
        if (_disableToggleEvent) return;

        if (enableSound)
        {
            PlayerPrefs.SetFloat(volumeParameter, volumeSlider.value);
            volumeSlider.value = volumeSlider.minValue;
        }
        else
        {

            volumeSlider.value = PlayerPrefs.GetFloat(volumeParameter, volumeSlider.value);
        }
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
        _disableToggleEvent = true;
        toggle.isOn = volumeSlider.value <= volumeSlider.minValue;
        _disableToggleEvent = false;
    }
}
