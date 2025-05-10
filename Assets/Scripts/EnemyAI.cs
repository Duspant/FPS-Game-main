using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] patrolTargets; // Points to patrol
    public float detectionRange = 10f;
    public Transform player;

    private int currentTargetIndex = 0;
    private NavMeshAgent navAgent;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        if (patrolTargets.Length > 0)
        {
            navAgent.destination = patrolTargets[currentTargetIndex].position;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            navAgent.destination = player.position;
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        if (patrolTargets.Length == 0) return;

        if (!navAgent.pathPending && navAgent.remainingDistance < 0.5f)
        {
            currentTargetIndex = (currentTargetIndex + 1) % patrolTargets.Length;
            navAgent.destination = patrolTargets[currentTargetIndex].position;
        }
    }
}
