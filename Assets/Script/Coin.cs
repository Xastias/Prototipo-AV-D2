using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Valor = 1;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.CompareTag("Player"))
        {
        //GameManager.Instance.MoreCoint(Valor);    
        GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true); //activa el objecto hijo del object "Poty"
        Destroy(gameObject, 0.5f); // Se destruyen los gameObjects en 5sg    
        }
    }
}
