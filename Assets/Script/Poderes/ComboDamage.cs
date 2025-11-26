using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboDamage : MonoBehaviour
{
    // Daño Combo
    public float Daño1_2;

    // DAMAGE COMBO
    private void OnTriggerEnter2D(Collider2D Combo12) 
    {
        if(Combo12.CompareTag("Enemy") && Combo12.GetComponent<VidaSkelt>())
        {
            Combo12.GetComponent<VidaSkelt>().DañoRag(Daño1_2);
            //Combo12.GetComponent<VidaSkelt>().cronometro = 1f;
        }
    }
}

