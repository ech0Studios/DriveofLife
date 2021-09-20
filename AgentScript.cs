
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour
{
    public static float thiefSpeed = 25;
    
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(transform.position.x, transform.position.y, transform.position.z + 600));              
    }
    void Update()
    {
        agent.speed = thiefSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Finish") || other.gameObject.CompareTag("GarbageGateTrigger") || other.gameObject.CompareTag("IceCreamGateTrigger") || other.gameObject.CompareTag("PoliceGateTrigger") || other.gameObject.CompareTag("ChaosGateTrigger"))
        {
            Destroy(gameObject);
        }
    }
}