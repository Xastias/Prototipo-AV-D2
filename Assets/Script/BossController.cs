
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int health = 200;
    public float speed = 2f;
    private Animator animator;
    private Transform player;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (health <= 0)
        {
            animator.SetTrigger("deathTrigger");
            return;
        }

        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > 2f)
        {
            animator.SetBool("isWalking", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetTrigger("attackTrigger");
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health > 0)
        {
            animator.SetTrigger("hurtTrigger");
        }
    }
}