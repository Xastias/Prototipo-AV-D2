using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriiggerAreaCheck : MonoBehaviour
{
    private Skelet_Enemy enemyparent;

    private void Awake()
    {
        enemyparent = GetComponentInParent<Skelet_Enemy>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            enemyparent.target = collider.transform;
            enemyparent.inRange = true;
            enemyparent.HotZone.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            enemyparent.inRange = false;
            enemyparent.HotZone.SetActive(false);
        }
    }
}
