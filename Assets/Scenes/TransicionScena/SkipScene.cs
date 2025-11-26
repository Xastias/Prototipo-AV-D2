<<<<<<< Updated upstream
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class SkipScene : MonoBehaviour
{
    [Header("UI")]
    public Button skipButton;         // Asignar en el inspector (UI Button)
    public TMP_Text buttonText;       // Opcional: texto dentro del botón (TextMeshPro)

    [Header("Comportamiento")]
    [Tooltip("Nombre de la escena a cargar al skipear. Si está vacío, desactivará el GameObject de la cinemática.")]
    public string sceneToLoad = "Tutorial";
    [Tooltip("Segundos antes de mostrar el prompt de 'Presiona Espacio' (útil si la cinemática comienza con un fade).")]
    public float showDelay = 1f;
    [Tooltip("Pequeña espera tras pulsar antes de cargar/desactivar para permitir animaciones.")]
    public float afterSkipDelay = 0.15f;

    [Header("Eventos")]
    public UnityEvent onSkip; // Opcional: asignar acciones adicionales en el inspector

    bool isSkipped = false;
    bool promptVisible = false;

    void Start()
    {
        // Asegúrate de ocultar al inicio
        SetPromptVisible(false);

        if (skipButton != null)
            skipButton.onClick.AddListener(OnButtonClick);

        if (showDelay > 0f)
            StartCoroutine(ShowPromptAfterDelay());
        else
            SetPromptVisible(true);
    }

    void OnDestroy() 
    {
        if (skipButton != null)
            skipButton.onClick.RemoveListener(OnButtonClick);
    }

    void Update()
    {
        if (isSkipped) return;

        // Detecta espacio. Puedes añadir KeyCode.Return u otras teclas si lo deseas.
        if (promptVisible && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PerformSkip());
        }
    }

    void OnButtonClick()
    {
        if (isSkipped) return;
        StartCoroutine(PerformSkip());
    }

    IEnumerator ShowPromptAfterDelay()
    {
        yield return new WaitForSeconds(showDelay);
        SetPromptVisible(true);
    }

    IEnumerator PerformSkip()
    {
        isSkipped = true;
        SetPromptVisible(false);

        // Evento para que otras partes reaccionen
        onSkip?.Invoke();

        // Espera breve para permitir animaciones/sonidos
        yield return new WaitForSeconds(afterSkipDelay);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Carga la escena especificada
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Si no se ha indicado escena, desactiva la cinemática (este GameObject)
            gameObject.SetActive(false);
        }
    }

    void SetPromptVisible(bool visible)
    {
        promptVisible = visible;

        if (skipButton != null)
            skipButton.gameObject.SetActive(visible);

        if (buttonText != null)
        {
            // Solo mostramos/ocultamos el texto; el contenido se gestiona manualmente en el Inspector.
            buttonText.gameObject.SetActive(visible);
        }
    }
}
=======
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class SkipScene : MonoBehaviour
{
    [Header("UI")]
    public Button skipButton;         // Asignar en el inspector (UI Button)
    public TMP_Text buttonText;       // Opcional: texto dentro del botón (TextMeshPro)

    [Header("Comportamiento")]
    [Tooltip("Nombre de la escena a cargar al skipear. Si está vacío, desactivará el GameObject de la cinemática.")]
    public string sceneToLoad = "Tutorial";
    [Tooltip("Segundos antes de mostrar el prompt de 'Presiona Espacio' (útil si la cinemática comienza con un fade).")]
    public float showDelay = 1f;
    [Tooltip("Pequeña espera tras pulsar antes de cargar/desactivar para permitir animaciones.")]
    public float afterSkipDelay = 0.15f;

    [Header("Eventos")]
    public UnityEvent onSkip; // Opcional: asignar acciones adicionales en el inspector

    bool isSkipped = false;
    bool promptVisible = false;

    void Start()
    {
        // Asegúrate de ocultar al inicio
        SetPromptVisible(false);

        if (skipButton != null)
            skipButton.onClick.AddListener(OnButtonClick);

        if (showDelay > 0f)
            StartCoroutine(ShowPromptAfterDelay());
        else
            SetPromptVisible(true);
    }

    void OnDestroy() 
    {
        if (skipButton != null)
            skipButton.onClick.RemoveListener(OnButtonClick);
    }

    void Update()
    {
        if (isSkipped) return;

        // Detecta espacio. Puedes añadir KeyCode.Return u otras teclas si lo deseas.
        if (promptVisible && Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PerformSkip());
        }
    }

    void OnButtonClick()
    {
        if (isSkipped) return;
        StartCoroutine(PerformSkip());
    }

    IEnumerator ShowPromptAfterDelay()
    {
        yield return new WaitForSeconds(showDelay);
        SetPromptVisible(true);
    }

    IEnumerator PerformSkip()
    {
        isSkipped = true;
        SetPromptVisible(false);

        // Evento para que otras partes reaccionen
        onSkip?.Invoke();

        // Espera breve para permitir animaciones/sonidos
        yield return new WaitForSeconds(afterSkipDelay);

        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            // Carga la escena especificada
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            // Si no se ha indicado escena, desactiva la cinemática (este GameObject)
            gameObject.SetActive(false);
        }
    }

    void SetPromptVisible(bool visible)
    {
        promptVisible = visible;

        if (skipButton != null)
            skipButton.gameObject.SetActive(visible);

        if (buttonText != null)
        {
            // Solo mostramos/ocultamos el texto; el contenido se gestiona manualmente en el Inspector.
            buttonText.gameObject.SetActive(visible);
        }
    }
}
>>>>>>> Stashed changes
