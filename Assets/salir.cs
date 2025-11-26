using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class salir : MonoBehaviour
{
    
private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Cambia a la escena del nivel 2
            SceneManager.LoadScene(5);
        }
    }

}
