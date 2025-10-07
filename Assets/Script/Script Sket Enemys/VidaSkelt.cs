using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para usar el Slider

public class VidaSkelt : MonoBehaviour
{
    [SerializeField] public GameObject Skelet_Enemy;
    private Animator anim;
    
    [Header("Parámetros de Vida")]
    [SerializeField] private float vidaMaxima = 40f;
    [SerializeField] private float vida; 
    [SerializeField] private Slider healthBarSlider; // Arrastra aquí el Slider de la vida del esqueleto
    [SerializeField] private GameObject efectoMuerte;

    [Header("Efectos Visuales y de Sonido")]
    public SpriteRenderer spriteRenderer;
    public AudioSource AS;
    public AudioClip hitSound;

    // Parámetros de la luz parpadeante
    private Color colorOriginal;
    float tiempoEncendido = 0.1f;
    float tiempoApagado = 0.1f;
    int cantidadParpadeos = 1;

    private void Start() 
    {
        vida = vidaMaxima; 
        anim = Skelet_Enemy.GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
        colorOriginal = spriteRenderer.color;
        ActualizarBarraDeVida();
    }

    public void DañoRag(float dañoT)
    {
        vida -= dañoT;
        ActualizarBarraDeVida();

        if (vida <= 0)
        {
            Muerte();
        }
        else
        {
            StartCoroutine(LuzParpadeante());
            spriteRenderer.color = Color.red;
            
            if (AS != null && hitSound != null)
            {
                AS.PlayOneShot(hitSound);
            }
        }
    }

    // --- FUNCIÓN CLAVE MODIFICADA ---
    public void Muerte()
    {
        // 1. Avisa al GameManager que el enemigo ha sido derrotado
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnEnemyDefeated();
        }

        // 2. Reproduce el efecto de muerte
        if (efectoMuerte != null)
        {
            Instantiate(efectoMuerte, transform.position, Quaternion.identity);
        }

        // 3. Destruye el objeto del esqueleto
        Destroy(gameObject, 0.2f);
    }

    private void ActualizarBarraDeVida()
    {
        if (healthBarSlider != null)
        {
            float porcentajeVida = vida / vidaMaxima;
            healthBarSlider.value = porcentajeVida;
        }
    }

    IEnumerator LuzParpadeante()
    {
        Color colorActual = spriteRenderer.color;
        for (int i = 0; i < cantidadParpadeos; i++)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(tiempoApagado);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(tiempoEncendido);
        }
        spriteRenderer.color = colorActual;
    }
}