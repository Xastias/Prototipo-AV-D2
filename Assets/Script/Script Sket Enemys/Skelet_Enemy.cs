using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelet_Enemy : MonoBehaviour
{
    #region Public variables
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform left_Limit;
    public Transform right_Limit;
    [HideInInspector] public Transform target;
    [HideInInspector] public bool inRange;
    public GameObject HotZone;
    public GameObject triggerArea;
    #endregion

    #region private variables
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool coolding;
    private float intTimer;
    #endregion

    private void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (!InsideoLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Ske_Attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            EnemyLogic();
        }
    }

    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);
        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && coolding == false)
        {
            Attack();
        }
        if (coolding)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("Walk", true);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Ske_Attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;
        anim.SetBool("Walk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && coolding && attackMode)
        {
            coolding = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        coolding = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    public void TriggerCooling()
    {
        coolding = true;
    }

    private bool InsideoLimits()
    {
        return transform.position.x > left_Limit.position.x && transform.position.x < right_Limit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, left_Limit.position);
        float distanceToRight = Vector2.Distance(transform.position, right_Limit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = left_Limit;
        }
        else
        {
            target = right_Limit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }
        transform.eulerAngles = rotation;
    }


}