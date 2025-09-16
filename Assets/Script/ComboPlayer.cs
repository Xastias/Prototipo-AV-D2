using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboPlayer : MonoBehaviour
{
    private Animator anim;
    public int combo;
    public bool attack;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        Combos();
    }

    public void StartCombo() 
    {
        attack = false;
        if (combo < 3)
        {
            combo++;
        }
    }

    public void Combos() 
    {
    if (Input.GetKeyDown(KeyCode.R) && !attack)
        {
            attack = true;
            anim.SetTrigger("" + combo);
        }    
    }

    public void FinishAnim() 
    {
        combo = 0;
        attack = false;
    }

}
