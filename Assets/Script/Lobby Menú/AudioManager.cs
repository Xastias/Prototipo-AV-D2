using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

[System.Serializable]
public class SceneMusic
{
    public string sceneName;      // Nombre exacto de la escena
    public AudioClip musicClip;   // Música asociada
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Mixer")]
    public AudioMixer mixer; // Asigna tu GameAudioMixer en el Inspector

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("Música por escena")]
    public SceneMusic[] sceneMusics;

    // Nombres de los parámetros expuestos en el Mixer
    private const string MUSIC_PARAM = "MusicVolume";
    private const string SFX_PARAM = "SFXVolume";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.activeSceneChanged += OnSceneChanged;

        // 👇 Detecta la escena actual al iniciar (por si no empezamos desde el Lobby)
        Scene currentScene = SceneManager.GetActiveScene();
        OnSceneChanged(currentScene, currentScene);
    }

    void Start()
    {
        float musicVol = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxVol = PlayerPrefs.GetFloat("SFXVolume", 1f);

        SetMusicVolume(musicVol);
        SetSFXVolume(sfxVol);
    }

    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    void OnDisable()
    {
        // Guarda en formato lineal (0..1)
        PlayerPrefs.SetFloat("MusicVolume", GetLinearVolume(MUSIC_PARAM));
        PlayerPrefs.SetFloat("SFXVolume", GetLinearVolume(SFX_PARAM));
        PlayerPrefs.Save();
    }

    // 🎵 Cambia la música según la escena
    private void OnSceneChanged(Scene previous, Scene next)
    {
        if (musicSource == null) return;

        // Busca si hay música asignada para esta escena
        foreach (var entry in sceneMusics)
        {
            if (entry.sceneName == next.name)
            {
                if (musicSource.clip != entry.musicClip)
                {
                    musicSource.clip = entry.musicClip;
                    musicSource.Play();
                }
                return; // encontrada, salimos
            }
        }

        // Si no hay música definida para esta escena, se detiene
        musicSource.Stop();
    }

    // ----------------------------
    // 🎚️ CONTROL DE VOLUMEN
    // ----------------------------

    public void SetMusicVolume(float linearVolume)
    {
        linearVolume = Mathf.Clamp01(linearVolume);
        if (linearVolume <= 0f)
            mixer.SetFloat(MUSIC_PARAM, -80f); // mute total
        else
            mixer.SetFloat(MUSIC_PARAM, Mathf.Log10(linearVolume) * 20f);
    }

    public void SetSFXVolume(float linearVolume)
    {
        linearVolume = Mathf.Clamp01(linearVolume);
        if (linearVolume <= 0f)
            mixer.SetFloat(SFX_PARAM, -80f);
        else
            mixer.SetFloat(SFX_PARAM, Mathf.Log10(linearVolume) * 20f);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip);
    }

    // Obtiene el volumen actual (0 a 1) desde el Mixer
    public float GetLinearVolume(string parameter)
    {
        if (mixer == null) return 1f;

        mixer.GetFloat(parameter, out float dB);
        float linear = Mathf.Pow(10f, dB / 20f);
        return Mathf.Clamp01(linear);
    }

    // Métodos de conveniencia
    public float GetMusicLinear() => GetLinearVolume(MUSIC_PARAM);
    public float GetSFXLinear() => GetLinearVolume(SFX_PARAM);
}
