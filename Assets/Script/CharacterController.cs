using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Componentes del Jugador")]
    public Animator anim;
    private Rigidbody2D rb;
    public SpriteRenderer sp;

    [Header("Estadísticas de Combate (Tutorial)")]
    public bool usedSword = false;
    public bool usedFireball = false;
    public float damageTaken = 0f;

    [Header("Movimiento")]
    public float speed = 5;
    public float jumpForce = 5;
    private BoxCollider2D BC;
    public LayerMask isGrounded;
    
    // Doble salto
    private int jumpCount = 0;
    private int maxJumps = 2;
    private float jumpCooldown = 0f;
    public float jumpCooldownDuration = 0f;
    private bool wasGrounded = false;
    private bool jumpPressed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BC = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // --- Rastrear el uso de habilidades ---
        // Bola de fuego con la tecla 'R'
        if (Input.GetKeyDown(KeyCode.R))
        {
            usedFireball = true;
            Debug.Log("Bola de fuego usada.");
        }

        // Espada con el clic izquierdo del ratón
        if (Input.GetMouseButtonDown(0))
        {
            usedSword = true;
            Debug.Log("Espada usada.");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        Movement();
        if (jumpCooldown > 0f)
            jumpCooldown -= Time.fixedDeltaTime;
        jumpPressed = false;
    }

    bool Grounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(BC.bounds.center, new Vector2(BC.bounds.size.x, BC.bounds.size.y), 0f, Vector2.down, 0.1f, isGrounded);
        return raycastHit.collider != null;
    }

    void Movement()
    {
        // Movimiento Player
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, speed, 0.2f), rb.velocity.y);
            sp.flipX = false;
            anim.SetBool("Run", true);
            RotateChildObjects(Quaternion.identity);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, -speed, 0.2f), rb.velocity.y);
            sp.flipX = true;
            anim.SetBool("Run", true);
            RotateChildObjects(Quaternion.Euler(0, 180, 0));
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, 0.2f), rb.velocity.y);
            anim.SetBool("Run", false);
        }

        void RotateChildObjects(Quaternion rotation)
        {
            foreach (Transform child in transform)
            {
                child.rotation = rotation;
            }
        }

        // Doble salto
        bool isGroundedNow = Grounded();

        if (isGroundedNow && !wasGrounded)
        {
            jumpCount = 0;
            anim.SetBool("Jump", false);
            jumpCooldown = 0f;
        }

        if (jumpPressed && jumpCount < maxJumps && jumpCooldown <= 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            anim.SetBool("Jump", true);
            jumpCooldown = jumpCooldownDuration;
        }
        else if (!isGroundedNow)
        {
            anim.SetBool("Jump", true);
            anim.SetBool("Run", false);
        }

        wasGrounded = isGroundedNow;
    }

    // --- Función para recibir daño ---
    // El enemigo deberá llamar a esta función para hacer daño al jugador
    public void TakeDamage(float damage)
    {
        damageTaken += damage;
        Debug.Log("Daño recibido. Total: " + damageTaken);
        // Aquí podrías añadir lógica de knockback, invulnerabilidad, etc.
    }
}