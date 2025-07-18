using UnityEngine;
using UnityEngine.AI;

public class AIenemy : MonoBehaviour
{
    public float detectionRange = 15f;

    private NavMeshAgent agent;
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found. Make sure your player has the 'Player' tag.");
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            agent.ResetPath(); // stop moving
        }
    }
}
