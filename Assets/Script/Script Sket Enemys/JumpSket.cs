using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpSket : MonoBehaviour
{
    public float jumpForce = 5f;
    public float raycastDistance = 1f;
    public LayerMask Layer;

    private void Update()
    {
        // Obtener la posición y dirección del frente del enemigo
        Vector2 frontPosition = transform.position + transform.right * raycastDistance;
        Vector2 raycastDirection = transform.right;

        Debug.DrawRay(transform.position, transform.right * raycastDistance ,Color.cyan);
        // Lanzar el raycast
        RaycastHit2D hit = Physics2D.Raycast(frontPosition, raycastDirection, raycastDistance, Layer);

        // Si se detecta un IsGrounded, saltar
        if (hit.collider != null)
        {
            Jump();
        }
    }

    private void Jump()
    {
        // Aplicar fuerza de salto al Rigidbody2D del enemigo
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
