using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerNOT : MonoBehaviour
{
    // Monedas
    public static GameManager Instance {get; private set;}
    public int TotalCoins {get {return   totalcoins;}}
    private int totalcoins;

    //Animación
    //public Animator anim;

    // Referencia al script Hud
    public HUD hud;

    // Vida 
    public int Lifes;

    private void Awake() 
    {
        if (Instance == null)
        {
            //Instance = this;
        }

    }
    //  Moneda (Coin)
    public void MoreCoint (int CoinsASumar)
    {
        totalcoins += CoinsASumar;
        hud.UpdateCoints(totalcoins);
    } 

    // Vida
    public void ReduceLife()
    {
        Lifes -= 1;
        
        if (Lifes == 0)
        {
            SceneManager.LoadScene(0);
        }

       hud.DisableLife(Lifes);

        // Reproducir la animación de golpe en el jugador
       //anim.SetTrigger("Hit"); ////AÚN FALTA PONER ESTA ANIMACIÓN
    }

    //Recuperar Vida
    public bool RecoverLife()
    {
        if (Lifes == 3)
        {
            return false;
        } 

        hud.EnableLife(Lifes);
        Lifes += 1;
        return true;
    }
}
