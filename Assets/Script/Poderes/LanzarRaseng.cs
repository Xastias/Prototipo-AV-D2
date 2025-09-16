using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzarRaseng : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] public float dañoT;

    private void Start()
    {
        Invoke("DestruirRaseng", 1f);
    }

    public void Update() 
    { // con esto lanzamos el rasengan hacia delante
        transform.Translate(Vector3.right * velocidad * Time.deltaTime);
    }


    public void OnTriggerEnter2D(Collider2D Raseng) 
    {
        if(Raseng.gameObject.CompareTag("Enemy"))
        {
            Raseng.GetComponent<VidaSkelt>().DañoRag(dañoT);
            Destroy(gameObject);
        }
    }

    private void DestruirRaseng()
    {
        Destroy(gameObject);
    }
}
