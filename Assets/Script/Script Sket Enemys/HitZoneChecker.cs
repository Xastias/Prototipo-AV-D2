using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitZoneChecker : MonoBehaviour
{
    private Skelet_Enemy enemyParent;
    private bool inRange;
    private Animator anim;

    private void Awake() {
        enemyParent = GetComponentInParent<Skelet_Enemy>();
        anim = GetComponentInParent<Animator>();
    }

    private void Update() {
        if(inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Ske_Attack"))
        {
            enemyParent.Flip();
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player")
        {
            inRange = true;
        }    
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        {
            inRange = false;
            gameObject.SetActive(false);
            enemyParent.triggerArea.SetActive(true);
            enemyParent.inRange = false;
            enemyParent.SelectTarget();
        }
    }


}
