using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject dieEffect;
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider healthBarSlider;
    private float health;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); 
        
        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = health;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (healthBarSlider != null)
        {
            healthBarSlider.value = health;
        }

        if (health > 0)
        {
            StartCoroutine(FlashEffect());
        }
        else
        {
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        StartCoroutine(GetDamage());
    }

    IEnumerator GetDamage()
    {
        float damageDuration = 0.1f;
        float damage = Random.Range(1f, 5f);
        
        // Llama al método TakeDamage para aplicar la lógica de vida y barra.
        TakeDamage(damage);

        // El resto del feedback visual
        if (health > 0)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(damageDuration);
            spriteRenderer.color = Color.white;
        }
        // La lógica de la muerte está en TakeDamage y ya se ejecutó si health <= 0
        else
        {
             
        }
    }

    IEnumerator FlashEffect()
    {
        float duration = 0.1f;
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }
}