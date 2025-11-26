using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    void Start()
    {
        // Seguridad: comprobar instancias
        if (AudioManager.Instance == null)
        {
           //ebug.LogWarning("AudioManager.Instance no está disponible.");
            return;
        }

        // Inicializa sliders desde el AudioMixer (valores lineales 0..1)
        if (musicSlider != null)
        {
            musicSlider.value = AudioManager.Instance.GetMusicLinear();
            musicSlider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = AudioManager.Instance.GetSFXLinear();
            sfxSlider.onValueChanged.AddListener(AudioManager.Instance.SetSFXVolume);
        }
    }

    void OnDestroy()
    {
        // Evitar listeners duplicados cuando se destruya el menú
        if (musicSlider != null && AudioManager.Instance != null)
            musicSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetMusicVolume);

        if (sfxSlider != null && AudioManager.Instance != null)
            sfxSlider.onValueChanged.RemoveListener(AudioManager.Instance.SetSFXVolume);
    }
}
