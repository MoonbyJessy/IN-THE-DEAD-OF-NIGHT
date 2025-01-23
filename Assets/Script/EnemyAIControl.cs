using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAIControl : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;

    //Patroling

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;


    //States
    public float sightRange;
    public bool playerInSightRange;


    //public PlayerSanity playerSanity;

    public enum EnemyState
    {
        Searching, Patrolling, Chasing
    }
    public EnemyState enemyState;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSightRange)
        {
            Patroling();
            Debug.Log("PlayerNOTinSight");
        }

        if (playerInSightRange)
        {
            ChasePlayer();
        }
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

    }
    private void SearchWalkPoint()
    {
        enemyState = EnemyState.Searching;

        //Calculate random area in range point
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {


            walkPointSet = true;

            enemyState = EnemyState.Patrolling;

        }

    }
    private void ChasePlayer()
    {
        enemyState = EnemyState.Chasing;
        agent.SetDestination(player.transform.position);
    }




    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(walkPoint, 1f);
    }
}

