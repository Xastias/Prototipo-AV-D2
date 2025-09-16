using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    public float minDamage = 7f; // Daño mínimo
    public float maxDamage = 10f; // Daño máximo

    private GameManager gameManager; // Referencia al GameManager

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InflictDamage();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Implementa cualquier lógica adicional aquí si es necesario cuando el jugador deja el área de daño
        }
    }

    private void InflictDamage()
    {
        float damageAmount = Random.Range(minDamage, maxDamage); // Generar un número aleatorio dentro del rango
        gameManager.ReduceLife(damageAmount); // Llama al método ReduceLife del GameManager
        
    }
}

