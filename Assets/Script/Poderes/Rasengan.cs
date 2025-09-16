 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rasengan : MonoBehaviour
{
    [SerializeField] private Transform  ControladorDisparo;
    [SerializeField] private GameObject Bullet;

    private bool canShoot = true;
    public float cooldown = 1f;
    private float lastShootTime;

    public Animator anim;
    
    public void Update()
{
    if (canShoot && Input.GetKeyDown(KeyCode.R))
    {
        Disparar();
        lastShootTime = Time.time;
        canShoot = false;
    }

    if (!canShoot && Time.time >= lastShootTime + cooldown)
    {
        canShoot = true;
    }
}

    // Funcion para lanzar y ejecutar la animacion del Katon
    public void Disparar()
{
    if (!canShoot)
    {
        return; // No se puede disparar debido al cooldown
    }

    anim.SetTrigger("Katon");
    Instantiate(Bullet, ControladorDisparo.position, ControladorDisparo.rotation);
}

}
