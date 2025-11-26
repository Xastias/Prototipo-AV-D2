<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerBootstrap : MonoBehaviour
{
    private static bool hasSpawned = false;

    void Awake()
    {
        // Evita duplicados si ya existe un AudioManager
        if (AudioManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Carga el prefab de AudioManager desde Resources o Prefabs
        if (!hasSpawned)
        {
            hasSpawned = true;

            AudioManager prefab = Resources.Load<AudioManager>("AudioManager");
            if (prefab == null)
            {
                // Si no está en Resources, intenta cargar desde Prefabs
                prefab = (AudioManager)Instantiate(Resources.Load("Prefabs/AudioManager"));
            }

            if (prefab == null)
            {
                Debug.LogError("No se encontró el prefab de AudioManager en Resources o Prefabs");
                return;
            }

            Instantiate(prefab);
        }

        Destroy(gameObject); // elimina este script después de instanciar AudioManager
    }
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerBootstrap : MonoBehaviour
{
    private static bool hasSpawned = false;

    void Awake()
    {
        // Evita duplicados si ya existe un AudioManager
        if (AudioManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Carga el prefab de AudioManager desde Resources o Prefabs
        if (!hasSpawned)
        {
            hasSpawned = true;

            AudioManager prefab = Resources.Load<AudioManager>("AudioManager");
            if (prefab == null)
            {
                // Si no está en Resources, intenta cargar desde Prefabs
                prefab = (AudioManager)Instantiate(Resources.Load("Prefabs/AudioManager"));
            }

            if (prefab == null)
            {
                Debug.LogError("No se encontró el prefab de AudioManager en Resources o Prefabs");
                return;
            }

            Instantiate(prefab);
        }

        Destroy(gameObject); // elimina este script después de instanciar AudioManager
    }
>>>>>>> Stashed changes
}