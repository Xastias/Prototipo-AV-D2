using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonesPrincipales : MonoBehaviour
{
    [Tooltip("Nombre exacto del objeto a mostrar/ocultar")]
    public string nombreObjetoAMostrar = "Fondo Opciones";
    public GameObject objetoAMostrar;

    void Awake()
    {
        //Buscar el objeto aunque esté desactivado
        if (objetoAMostrar == null)
        {
            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                if (obj.name == nombreObjetoAMostrar)
                {
                    objetoAMostrar = obj;
                    break;
                }
            }
        }

        //Asegurar que empieza desactivado
        if (objetoAMostrar != null)
            objetoAMostrar.SetActive(false);
    }

    void Update()
    {
        if (objetoAMostrar == null)
            return;

        int escenaActual = SceneManager.GetActiveScene().buildIndex;

        //Fuera del Lobby (escena 0): activar/desactivar con ESC
        if (escenaActual != 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                objetoAMostrar.SetActive(!objetoAMostrar.activeSelf);
            }
        }
        else
        {
            //En Lobby: se mantiene oculto a menos que se abra con el botón
            if (objetoAMostrar.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                objetoAMostrar.SetActive(false);
            }
        }
    }

    //Método para el botón "Opciones" del Canvas en la escena Lobby
    public void MostrarOpcionesLobby()
    {
        // Solo funciona si estás en la escena Lobby (índice 0)
        if (SceneManager.GetActiveScene().buildIndex == 0 && objetoAMostrar != null)
        {
            objetoAMostrar.SetActive(true);
        }
    }

    //Método para cerrar la ventana de opciones en el Lobby y las otras escenas
    public void OcultarOpcionesLobby()
    {
        if (objetoAMostrar != null)
        {
            objetoAMostrar.SetActive(false);
        }
    }

    //Método público para cambiar a la escena "Tutorial"
    public void EscenaTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    // Poner o quitar la pantalla completa
    public void PonerPantallaCompleta(bool pantallaCompleta)
    {
        
        if (pantallaCompleta)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }
    }

    public void AlternarPantallaCompleta()
    {
        PonerPantallaCompleta(!Screen.fullScreen);
    }
}