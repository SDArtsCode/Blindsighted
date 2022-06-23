using UnityEngine.AI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        agent.SetDestination(PlayerMovement.playerPosition);
    }
}
