
using UnityEngine;
using UnityEngine.SceneManagement;

public class salirNivel2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(6); // Usa el índice o nombre exacto del Nivel 3
        }
    }
}
