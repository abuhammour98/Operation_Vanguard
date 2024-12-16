using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum State
    {
        None,
        Patrol,
        Chase
    }

    public Transform target; // The target the enemy will chase (e.g., player)
    public float patrolSpeed = 2f; // Speed of patrol movement
    public float chaseSpeed = 5f; // Speed when chasing the target
    public float chaseRange = 10f; // Range within which the enemy will start chasing
    public State curState;

    void Start()
    {
        curState = State.Patrol;
    }

    void Update()
    {
        switch (curState)
        {
            case State.Patrol:
                UpdatePatrol();
                CheckForChase(); // Check if the target is within chase range
                break;
            case State.Chase:
                UpdateChase();
                CheckForPatrol(); // Check if the target is out of chase range
                break;
        }
    }

    void UpdatePatrol()
    {
        // Enemy rotates around a central point at patrolSpeed
        transform.RotateAround(Vector3.zero, Vector3.up, patrolSpeed * Time.deltaTime);
    }

    void UpdateChase()
    {
        if (target != null)
        {
            // Move towards the target at chaseSpeed
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * chaseSpeed * Time.deltaTime;
        }
    }

    void CheckForChase()
    {
        // Switch to Chase state if target is within range
        if (target != null && Vector3.Distance(transform.position, target.position) < chaseRange)
        {
            curState = State.Chase;
        }
    }

    void CheckForPatrol()
    {
        // Switch back to Patrol state if target is out of range
        if (target != null && Vector3.Distance(transform.position, target.position) >= chaseRange)
        {
            curState = State.Patrol;
        }
    }
}
