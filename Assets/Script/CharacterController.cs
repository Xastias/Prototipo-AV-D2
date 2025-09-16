using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rb;
    public SpriteRenderer sp;

    // Movimiento
    float horizontal;
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
    private bool jumpPressed = false; // Nueva variable

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        BC = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Detecta la pulsación de salto en Update
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
        jumpPressed = false; // Resetea la variable después de procesar
    }

    bool Grounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(BC.bounds.center, new Vector2(BC.bounds.size.x, BC.bounds.size.y), 0f, Vector2.down, 0.1f, isGrounded);
        return raycastHit.collider != null;
    }

    void Movement ()
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

        // Usa la variable jumpPressed en vez de Input.GetKeyDown
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
}