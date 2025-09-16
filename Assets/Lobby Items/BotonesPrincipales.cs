using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesPrincipales : MonoBehaviour
{

    public void Jugar()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}
