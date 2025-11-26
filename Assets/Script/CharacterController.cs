using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Componentes del Jugador")]
    public Animator anim;
    private Rigidbody2D rb;
    public SpriteRenderer sp;

    [Header("Sonido")]
    public AudioSource Pasos;
    public float tiempoEntrePasos = 0.4f; // intervalo entre sonidos
    private float contadorPasos = 0f;
    public AudioSource SonidoSalto;


    [Header("Estadísticas de Combate (Tutorial)")]
    public bool usedSword = false;
    public bool usedFireball = false;
    public float damageTaken = 0;

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

    private Coroutine swordCoroutine;
    private Coroutine fireballCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BC = GetComponent<BoxCollider2D>();
        Pasos = GetComponent<AudioSource>();
        if (Pasos == null) Pasos = GetComponent<AudioSource>();
        // Si hay otro AudioSource para salto, no hace falta asignarlo aquí.
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            CastFireball();
        }

        if (Input.GetMouseButtonDown(0))
        {
            AttackWithSword();
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
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            BC.bounds.center,
            new Vector2(BC.bounds.size.x, BC.bounds.size.y),
            0f,
            Vector2.down,
            0.1f,
            isGrounded
        );
        return raycastHit.collider != null;
    }

    void Movement()
    {
        bool isGroundedNow = Grounded();
        bool movingHorizontally = Input.GetKey("a") || Input.GetKey("left") || Input.GetKey("d") || Input.GetKey("right");

        float horizontalInput = 0;
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            horizontalInput = 1;
            sp.flipX = false;
            RotateChildObjects(Quaternion.identity);
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            horizontalInput = -1;
            sp.flipX = true;
            RotateChildObjects(Quaternion.Euler(0, 180, 0));
        }

        // --- Movimiento ---
        rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, horizontalInput * speed, 0.2f), rb.velocity.y);

        // --- Animaciones ---
        anim.SetBool("Run", movingHorizontally && isGroundedNow);
        anim.SetBool("Jump", !isGroundedNow);

        // --- Sonido de pasos ---
        if (movingHorizontally && isGroundedNow)
        {
            contadorPasos -= Time.fixedDeltaTime;
            if (contadorPasos <= 0f)
            {
                Pasos.Play();
                contadorPasos = tiempoEntrePasos;
            }
        }
        else
        {
            contadorPasos = 0f; // reiniciar cuando no camina o salta
        }

        // --- Salto ---
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

            // Detenemos los pasos al saltar
            if (Pasos.isPlaying)
                Pasos.Stop();

            // Reproducir sonido de salto (solo una vez por salto)
            if (SonidoSalto != null)
                SonidoSalto.Play();
        }

        wasGrounded = isGroundedNow;

        // --- Rotación hijos ---
        void RotateChildObjects(Quaternion rotation)
        {
            foreach (Transform child in transform)
            {
                child.rotation = rotation;
            }
        }
    }

    // --- Función para recibir daño ---
    public void TakeDamage(float damage)
    {
        damageTaken += damage;
    }

    // --- Ataque con espada ---
    public void AttackWithSword()
    {
        usedSword = true;
        if (swordCoroutine != null) StopCoroutine(swordCoroutine);
        swordCoroutine = StartCoroutine(ResetUsedSword());
    }

    // --- Lanzamiento de fireball ---
    public void CastFireball()
    {
        usedFireball = true;
        if (fireballCoroutine != null) StopCoroutine(fireballCoroutine);
        fireballCoroutine = StartCoroutine(ResetUsedFireball());
    }

    private IEnumerator ResetUsedSword()
    {
        yield return new WaitForSeconds(5f);
        usedSword = false;
    }

    private IEnumerator ResetUsedFireball()
    {
        yield return new WaitForSeconds(5f);
        usedFireball = false;
    }
}
