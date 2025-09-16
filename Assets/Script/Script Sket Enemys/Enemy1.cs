using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    [SerializeField]private float TiempoEntreDaño;
    private float TiempoSiguienteDaño;
    private bool isPlayerInside;

void Update()
{
    if (isPlayerInside)
    {
        TiempoSiguienteDaño -= Time.deltaTime;
        if (TiempoSiguienteDaño <= 0)
        {
            //GameManager.Instance.ReduceLife();
            TiempoSiguienteDaño = TiempoEntreDaño;
        }
    }
}

void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.gameObject.tag == "Player")
    {
        isPlayerInside = true;
    }
}

void OnTriggerExit2D(Collider2D collision)
{
    if (collision.gameObject.tag == "Player")
    {
        isPlayerInside = false;
    }
}

}
