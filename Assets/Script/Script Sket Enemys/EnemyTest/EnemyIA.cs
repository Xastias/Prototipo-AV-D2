using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    
    #region Public variables
    public Collider2D detectionCollider;
    public Transform player;
    public float movementSpeed = 5f;
    public float attackCooldown = 2f;
    public SpriteRenderer spriteRenderer;
    #endregion

    #region private variables
    private bool isAttacking = false;
    private float attackTimer = 0f;
    private Transform Player;

    private Rigidbody2D rb;
    private Animator animator;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isAttacking)
        {
            if (attackTimer <= 0)
            {
                // Realizar el ataque
                attackTimer = attackCooldown;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }

        }
        Attack();
    }

    private void Attack()
    {
        if (player != null && !isAttacking)
        {
            // Girar el sprite hacia el jugador
            if (transform.position.x < player.position.x)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;

            // Moverse hacia el jugador
            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * movementSpeed * Time.deltaTime;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = true;
            animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttacking = false;
            animator.ResetTrigger("Attack");
        }
    }

    private void OnEnable()
    {
        if (detectionCollider != null)
            detectionCollider.isTrigger = true;
    }

    private void OnDisable()
    {
        if (detectionCollider != null)
            detectionCollider.isTrigger = false;
    }
}