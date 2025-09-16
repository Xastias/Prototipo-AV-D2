using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaSkelt : MonoBehaviour
{
    [SerializeField] public GameObject Skelet_Enemy;
    private Animator anim;
    [SerializeField] private float vida; 
    [SerializeField] private GameObject efectoMuerte;

    [Header("Light Shader")]
    /*public float time_light;
    public SpriteRenderer[] spr;
    public bool change;
    public Color[]color_;
    public float speed_shine;
    public float cronometro;*/

    // Parámetros de la luz parpadeante
    private Color colorOriginal;
    public SpriteRenderer spriteRenderer;
    float tiempoEncendido = 0.1f;
    float tiempoApagado = 0.1f;
    int cantidadParpadeos = 1;

    public AudioSource AS;
    public AudioClip hitSound;

    private void Start() 
    {
        anim = Skelet_Enemy.GetComponent<Animator>();
        AS = GetComponent<AudioSource>();
        // Guardar el color original del SpriteRenderer
        colorOriginal = spriteRenderer.color;
    }

    public void DañoRag(float dañoT)
    {
        vida -= dañoT;

        if (vida <= 0)
        {
            Muerte();
        }
        else
        {
            StartCoroutine(LuzParpadeante());
            // Código para modificar el SpriteRenderer (por ejemplo, cambiar el color)
            spriteRenderer.color = Color.red;
            AS.Play();
        }
    }

    public void Muerte()
    {
    Instantiate(efectoMuerte, transform.position, Quaternion.identity);
    Destroy(gameObject, 0.2f);
    }

IEnumerator LuzParpadeante()
{
    // Guardar el color actual del SpriteRenderer
    Color colorActual = spriteRenderer.color;

    // Realizar el parpadeo
    for (int i = 0; i < cantidadParpadeos; i++)
    {
        spriteRenderer.enabled = false; // Apagar el SpriteRenderer
        yield return new WaitForSeconds(tiempoApagado);

        spriteRenderer.enabled = true; // Encender el SpriteRenderer
        yield return new WaitForSeconds(tiempoEncendido);
    }

    // Restaurar el color original del SpriteRenderer
    spriteRenderer.color = colorOriginal;
}

   /* public void Light () 
    {
        if(cronometro > 0)
        {
            cronometro -= 1 * Time.deltaTime;
            spr[1].sprite = spr[0].sprite;
            time_light += speed_shine * Time.deltaTime;

            switch (change)
            {   
            case true:

                spr[1].color = color_[0];
                break;

            case false:

                spr[1].color = color_[1];
                break;
            }

            if(time_light > 1)
            {
                change = !change;
                time_light = 0;
            }
            else
            {
                cronometro = 0;
                spr[1].color = color_[0];
            }
        }
    }*/
}

