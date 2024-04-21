using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float minPatrolTime = 5;
    public float maxPatrolTime = 10;
    public float patrolRadius = 20;

    private NavMeshAgent agent;
    private Transform Target;
    private Animator animator;
    private Health health;

    private enum State
    {
        PATROL,
        AGRO
    }

    private State state;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        state = State.PATROL;

        StartCoroutine(Patrol());
    }

    void Update()
    {
        switch (state)
        {
            case State.PATROL:

                break;

        }


        if (agent.velocity.magnitude > 0)
        {
            animator.SetBool("Slither Forward", true);
        }
        else{
            animator.SetBool("Slither Forward", false);
        }
    }


    private IEnumerator Patrol()
    {
        if (state == State.PATROL)
        {
            Vector3 offset = Random.insideUnitSphere * patrolRadius;
            offset.y = 0;
            offset += transform.position;
            agent.isStopped = false;
            agent.SetDestination(offset);

            yield return new WaitForSeconds(Random.Range(minPatrolTime, maxPatrolTime));
            StartCoroutine(Patrol());
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Damage(int amount)
    {
        if (!health.dead){
            health.Damage(amount);
            if (health.health <= 0) // Die
            {
                animator.SetTrigger("Die");
                agent.isStopped = true;
                agent.velocity = Vector3.zero;
                health.dead = true;
            }
            else
            {
                animator.SetTrigger("Take Damage");
            }
        }
    }
}
