using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VentanasTutos : MonoBehaviour
{
    public bool VentanaG;
    [SerializeField] private GameObject Mensaje2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            VentanaG = true;
            Mensaje2.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            VentanaG = false;
            Mensaje2.SetActive(false);
        }
    }
}
