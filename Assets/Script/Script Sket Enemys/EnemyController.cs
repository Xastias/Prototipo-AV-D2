using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    private float minY, maxY;
    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject[] enemigos;
    [SerializeField] private float tiempoEnemigos;
    [SerializeField] private int oleadas;
    [SerializeField] private float tiempoEntreOleadas;

    [Header("Publicas")]public int oleadaActual = 0;
    public float tiempoSiguienteEnemigo;
    private bool spawning = false;

    void Start()
    {
        maxY = puntos.Max(punto => punto.position.y);
        minY = puntos.Min(punto => punto.position.y);

        StartSpawning();
    }

    void Update()
    {
        if (spawning)
        {
            tiempoSiguienteEnemigo += Time.deltaTime;

            if (tiempoSiguienteEnemigo >= tiempoEnemigos)
            {
                tiempoSiguienteEnemigo = 0;
                SpawnWave();
            }
        }
    }

    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemiesInWaves());
    }

    IEnumerator SpawnEnemiesInWaves()
    {
        spawning = true;
        while (oleadaActual < oleadas)
        {
            yield return StartCoroutine(SpawnWave());
            yield return new WaitForSeconds(tiempoEntreOleadas);
            oleadaActual++;
        }
        spawning = false;
    }

    IEnumerator SpawnWave()
    {
    int numeroEnemigo = Random.Range(0, enemigos.Length);
    int numeroPunto = Random.Range(0, puntos.Length);
    Transform puntoSpawn = puntos[numeroPunto];

    int enemigosSpawned = 0;
    int enemigosPorOleada = 5 + (5 * oleadaActual); // Cantidad de enemigos por oleada

    while (enemigosSpawned < enemigosPorOleada)
        {
            float posicionY = Random.Range(minY, maxY) + Random.Range(-1f, 1f);
            Vector2 posicionAleatoria = new Vector2(puntoSpawn.position.x, posicionY);
            Instantiate(enemigos[numeroEnemigo], posicionAleatoria, Quaternion.identity);
            Instantiate(enemigos[numeroEnemigo], posicionAleatoria, Quaternion.identity);
            enemigosSpawned++;

            enemigosSpawned++;
            yield return new WaitForSeconds(tiempoEnemigos);
        }
    }
}

/* CÓDIGO ORIGINAL SIN WAVE
public class EnemyController : MonoBehaviour
{

    private float minX, maxX minY, maxY;
    [SerializeField] private Transform[] puntos;
    [SerializeField] private GameObject[] enemigos;
    [SerializeField] private float tiempoEnemigos;
    private float tiempoSiguienteEnemigo;

    void Start()
    {
       // maxX = puntos.Max(puntos => puntos.position.x);
       // minX = puntos.Min(puntos => puntos.position.x);
        maxY = puntos.Max(puntos => puntos.position.y);
        minY = puntos.Min(puntos => puntos.position.y);
    }

    void Update()
    {
        tiempoSiguienteEnemigo += Time.deltaTime;

        if(tiempoSiguienteEnemigo >= tiempoEnemigos)
        {
            tiempoSiguienteEnemigo = 0;
            Enemigo();
        }
    }


    private void Enemigo () 
    {
        int numeroEnemigo = Random.Range(0, enemigos.Length);
        float posicionY = Random.Range(minY, maxY);
        Vector2 posicionAleatoria = new Vector2(0, posicionY);
        Instantiate(enemigos[numeroEnemigo], posicionAleatoria, Quaternion.identity);

    }
}*/

