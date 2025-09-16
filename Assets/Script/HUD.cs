using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{

    public GameObject[] Life;
    public TextMeshProUGUI puntos;

    void Update()
    {
        //puntos.text = GameManager.Instance.TotalCoins.ToString();
    }

    public void UpdateCoints (int totalcoints) 
    {
        puntos.text = totalcoints.ToString();
    }

  public void DisableLife(int indice)
    {
        Life[indice].SetActive(false);
    }

    public void EnableLife(int indice)
    {
        Life[indice].SetActive(true);
    }
}
