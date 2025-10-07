using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro; // <-- ¡IMPORTANTE! Añadido para usar TextMeshPro

public class GameManager : MonoBehaviour
{
    // --- SINGLETON ---
    public static GameManager Instance { get; private set; }

    [Header("Referencias al Jugador")]
    [SerializeField] private CharacterController player;

    [Header("Sistema de Vida del Jugador")]
    [SerializeField] private float maxHealth = 30f;
    [SerializeField] private float currentHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private Text healthText; // La barra de vida sí puede usar el Text normal

    [Header("UI de Fin de Tutorial")]
    [SerializeField] private GameObject endScreenPanel;
    [SerializeField] private TextMeshProUGUI classNameText; // <-- CAMBIADO a TextMeshProUGUI
    [SerializeField] private TextMeshProUGUI classDescriptionText; // <-- CAMBIADO a TextMeshProUGUI
    [SerializeField] private Image classIconImage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        if (endScreenPanel != null)
        {
            endScreenPanel.SetActive(false);
        }
    }

    void Update()
    {
        UpdateHealthUI();
    }

    // --- LÓGICA DE VIDA ---
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public bool RecoverLife()
    {
        if (currentHealth >= maxHealth) return false;
        currentHealth = Mathf.Min(currentHealth + 10, maxHealth);
        return true;
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null) healthBar.fillAmount = currentHealth / maxHealth;
        if (healthText != null) healthText.text = currentHealth.ToString("f0") + "/" + maxHealth.ToString("f0");
    }

    // --- LÓGICA DE FIN DE TUTORIAL ---
    public void OnEnemyDefeated()
    {
        Debug.Log("Enemigo derrotado. Evaluando rendimiento...");
        EvaluatePlayerPerformance();
    }

    void EvaluatePlayerPerformance()
    {
        string className = "Milagroso";
        string description = "¿Cómo lo hiciste? La victoria cayó del cielo. Eres un enigma.";

        if (player.usedSword && player.usedFireball)
        {
            className = "Caballero Mágico";
            description = "Un guerrero versátil que combina el acero con el arcano. Equilibrado y letal.";
        }
        else if (player.usedFireball)
        {
            className = (player.damageTaken > 0) ? "Berserker Arcano" : "Mago";
            description = (player.damageTaken > 0) ? 
                "Un mago que se sumerge en el caos, soportando dolor para desatar un poder devastador." :
                "Un erudito de las artes arcanas. Derrotó a su enemigo desde una distancia segura, con precisión impecable.";
        }
        else if (player.usedSword)
        {
            className = (player.damageTaken > 0) ? "Caballero" : "Asesino";
            description = (player.damageTaken > 0) ? 
                "Un guerrero resistente y honorable. Sostuvo su terreno y absorbió el golpe del enemigo." :
                "Un maestro del sigilo y la precisión. Abatió a su objetivo sin recibir un solo rasguño.";
        }

        ShowEndScreen(className, description);
    }

    void ShowEndScreen(string name, string description)
    {
        Time.timeScale = 0f;
        if (classNameText != null) classNameText.text = "¡Tu clase es: " + name;
        if (classDescriptionText != null) classDescriptionText.text = description;
        if (endScreenPanel != null) endScreenPanel.SetActive(true);
    }

    public void OnContinueButtonClicked()
    {
        Time.timeScale = 1f;
        Debug.Log("Reiniciando el tutorial...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}