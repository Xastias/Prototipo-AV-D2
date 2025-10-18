#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

// Inserta automáticamente prefabs persistentes (AudioManager, HUD, Player, Cámara, etc.) en cada escena sin necesidad de darle Play.
// Evita duplicados y elimina la cámara por defecto de Unity.
[InitializeOnLoad]
public static class PersistentObjectsUtility
{
    // Prefabs persistentes a insertar automáticamente
    private static readonly string[] persistentPrefabs = {
        "Assets/Resources/AudioManager.prefab",
        "Assets/Resources/HUD.prefab",
        "Assets/Resources/PLAYER.prefab",
        "Assets/Resources/Main Camera.prefab",
        "Assets/Resources/CamaraLimit.prefab",
        "Assets/Resources/CM vcam1.prefab",
        "Assets/Resources/Menu Opciones.prefab",
        "Assets/Resources/EventSystem.prefab"
    };

    // Escenas donde NO se agregan estos objetos
    private static readonly string[] excludedScenes = {
        "Lobby"
    };

    // Bandera para evitar ejecuciones múltiples simultáneas
    private static bool isProcessing = false;

    static PersistentObjectsUtility()
    {
        EditorSceneManager.sceneOpened += OnSceneOpened;
    }

    private static void OnSceneOpened(Scene scene, OpenSceneMode mode)
    {
        if (Application.isPlaying) return;
        if (isProcessing) return;

        isProcessing = true;
        EditorApplication.delayCall += () =>
        {
            // Espera un poco más para evitar conflicto con el render
            EditorApplication.delayCall += () =>
            {
                try
                {
                    AddPersistentObjects(scene);
                }
                finally
                {
                    isProcessing = false;
                }
            };
        };
    }

    private static void AddPersistentObjects(Scene scene)
    {
        if (excludedScenes.Contains(scene.name))
            return;

        // Eliminar cámaras por defecto de Unity (si existen)
        var defaultCameras = Object.FindObjectsOfType<Camera>()
            .Where(c => c.gameObject.name == "Main Camera" || c.gameObject.name == "MainCamera")
            .ToArray();

        foreach (var cam in defaultCameras)
        {
            //Debug.Log($" Eliminada cámara por defecto: {cam.gameObject.name}");
            Object.DestroyImmediate(cam.gameObject);
        }

        // Agregar prefabs persistentes solo si no existen
        foreach (string prefabPath in persistentPrefabs)
        {
            GameObject prefab = LoadPrefab(prefabPath);
            if (prefab == null)
            {
                //Debug.LogWarning($" Prefab no encontrado: {prefabPath}");
                continue;
            }

            string prefabName = prefab.name;

            // Verificar si ya existe (tanto activos como inactivos)
            GameObject existing = Object.FindObjectsOfType<GameObject>(true)
                .FirstOrDefault(go => go.name == prefabName);

            if (existing != null)
                continue; // ya existe, no duplicar

            GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab, scene);
            instance.name = prefabName;

            // Si es la cámara, asegurar tag y configuración
            if (instance.TryGetComponent(out Camera cam))
            {
                instance.tag = "MainCamera";
                cam.enabled = true;
            }

            Undo.RegisterCreatedObjectUndo(instance, "Add Persistent Object");
            //Debug.Log($" '{prefabName}' agregado a la escena '{scene.name}'");
        }
    }

    // Carga un prefab desde Resources o directamente desde Assets.
    private static GameObject LoadPrefab(string prefabPath)
    {
        GameObject prefab = null;

        if (prefabPath.StartsWith("Assets/Resources/"))
        {
            string resourceName = prefabPath
                .Replace("Assets/Resources/", "")
                .Replace(".prefab", "");
            prefab = Resources.Load<GameObject>(resourceName);
        }
        else
        {
            prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
        }

        return prefab;
    }
}
#endif
