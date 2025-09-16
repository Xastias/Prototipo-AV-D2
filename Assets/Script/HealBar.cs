using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealBar : MonoBehaviour
{

    public float LifeAct;
    public float MaxLife = 100f;

    [Header("Interface")]
    public Image HealBarra;
    public Text TextLife;


    void Update()
    {
        UpdatingInterface();
    }

    void ReduceLife(float Daño)
    {
        LifeAct -= Daño;

        if (LifeAct == 0)
        {
            SceneManager.LoadScene(0);
        }
    }

        public bool RecoverLife()
    {
        if (LifeAct == 100)
        {
            return false;
        } 

        LifeAct += 10;
        return true;
    }


    void UpdatingInterface()
    {
        HealBarra.fillAmount = LifeAct / MaxLife;
        TextLife.text = "100/" + LifeAct.ToString("f0");
    }

}
