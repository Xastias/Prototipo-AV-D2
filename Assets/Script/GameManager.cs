using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    [SerializeField] private float LifeAct;
    [SerializeField] private float MaxLife;

    [Header("Interface")]
    public Image HealBarra;
    public Text TextLife;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

    }

    void Update()
    {
        UpdatingInterface();
    }

    public void ReduceLife(float Daño)
    {
        LifeAct -= Daño;

        if (LifeAct == 0)
        {
            SceneManager.LoadScene(0);
        }
    }

        public bool RecoverLife()
    {
        if (LifeAct == 30)
        {
            return false;
        } 

        LifeAct += 10;
        return true;
    }


    void UpdatingInterface()
    {
        HealBarra.fillAmount = LifeAct / MaxLife;
        TextLife.text = "30/" + LifeAct.ToString("f0");
    }

}
