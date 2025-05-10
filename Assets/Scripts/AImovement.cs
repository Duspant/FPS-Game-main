using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class AImovement : MonoBehaviour
{

    public Transform Target;
    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            agent.SetDestination(Target.position);
        }
    }
}
