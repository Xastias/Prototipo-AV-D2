using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotyVida : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
            bool LifeRecovery = GameManager.Instance.RecoverLife();
            
            if (LifeRecovery)
            {
                GetComponent<SpriteRenderer>().enabled = false; // Escondemos el objecto "poty" al tocarlo
                gameObject.transform.GetChild(0).gameObject.SetActive(true); //activa el objecto hijo del object "Poty"
                Destroy(gameObject, 0.5f); // Se destruyen los gameObjects en 5sg
            }
        }   
    }
}