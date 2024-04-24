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

    public GameObject spitPrefab;
    public Transform spitSpawnPoint;

    private NavMeshAgent agent;
    private Transform Target;
    private Animator animator;
    private Health health;

    private Transform target = null;

    private Coroutine attack;

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
        attack = StartCoroutine(checkAgro());
    }

    void Update()
    {

        if (state == State.AGRO && target != null)
        {
            transform.LookAt(target.position);
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
        }

        yield return new WaitForSeconds(Random.Range(minPatrolTime, maxPatrolTime));
        StartCoroutine(Patrol());
    }

    public void Die()
    {
        health.Kill();
        FindAnyObjectByType<PlayerController>().AddScore(100);
        Destroy(gameObject);
    }

    public void Damage(int amount)
    {
        if (!health.dead){
            health.Damage(amount);
            if (health.health <= 0) // Die
            {
                animator.SetTrigger("Die");
                StopAllCoroutines();
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
            state = State.PATROL;
            Debug.Log("Patrolling...");
        }
    }



    private IEnumerator checkAgro()
    {
        if (target != null)
        {
            RaycastHit hit;
            if (Physics.Linecast(transform.position, target.position, out hit))
            {
                if (hit.transform.CompareTag("Player") && state == State.PATROL){
                    Debug.Log("Agroed");
                    state = State.AGRO;
                    agent.isStopped = true;
                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                    StopCoroutine(attack);
                    attack = StartCoroutine(Attack());
                }
            }
            else
            {
                if (state == State.AGRO)
                {
                    Debug.Log("Patrolling...");
                }
                state = State.PATROL;
            }
        }

        yield return new WaitForSeconds(0.1f);
        StartCoroutine(checkAgro());
    }

    private IEnumerator Attack()
    {
        animator.SetTrigger("Spit Attack");

        yield return new WaitForSeconds(2.5f);

        if (state == State.AGRO)
        {
            StartCoroutine(Attack());
        }
    }

    public void Spit()
    {
        Instantiate(spitPrefab, spitSpawnPoint.position, Quaternion.identity).transform.LookAt(target);
    }
}
