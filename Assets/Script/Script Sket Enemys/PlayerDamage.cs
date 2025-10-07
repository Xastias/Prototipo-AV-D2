using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [Header("Configuración de Daño")]
    public float minDamage = 7f; // Daño mínimo
    public float maxDamage = 10f; // Daño máximo

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobar si el objeto que entra en el trigger es el jugador
        if (other.CompareTag("Player"))
        {
            // Asegurarse de que el GameManager existe antes de usarlo
            if (GameManager.Instance != null)
            {
                // Calcular un daño aleatorio entre el mínimo y el máximo
                float damageAmount = Random.Range(minDamage, maxDamage);
                
                // --- CORRECCIÓN CLAVE ---
                // Llamar a la función CORRECTA del GameManager: TakeDamage
                GameManager.Instance.TakeDamage(damageAmount);
                
                // --- MEJORA IMPORTANTE ---
                // Destruir este objeto después de hacer daño.
                // Esto evita que el jugador reciba daño continuamente cada frame
                // mientras permanezca en el área de ataque del enemigo.
                Destroy(gameObject);
            }
        }
    }
}