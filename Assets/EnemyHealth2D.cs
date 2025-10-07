using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealth2D : MonoBehaviour
{
    public float maxHealth = 50f; 
    public Slider healthBarSlider; 

    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = currentHealth;
        }
    }

    
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

       
        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
        }

      

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
       
        Debug.Log(gameObject.name + " ha sido derrotado en 2D.");
        Destroy(gameObject);
    }
}