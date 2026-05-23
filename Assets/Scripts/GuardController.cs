using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{

    public enum State { Patrol, Chase, Return, Random, Move}
    public State currentState = State.Move;
    [SerializeField] private GameObject Player;

    public Vector3 target;

    public GameObject Checkpoint;
    public int randomness;

    private NavMeshAgent agent;
    private MeshRenderer mesh;
     public Vector3 LastKnownLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         agent = GetComponent<NavMeshAgent>();
         target = transform.position;
         LastKnownLocation = Vector3.zero;
         mesh = GetComponent<MeshRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Move:
                agent.speed = 3.5f;
                 if (Vector3.Distance(transform.position, target) <= 2)
                {
                    Debug.Log("Switch");
                    int r = Random.Range(0,10);
                    if (r < randomness)
                    {
                        currentState = State.Random;
                        break;
                    }
                    else 
                    {
                        currentState = State.Patrol;
                        break;
                    }
                }
                break;
            case State.Random:
                mesh.materials[0].color = Color.purple;
                if (Vector3.Distance(transform.position, target) <= 1)
                {
                    target = RandomNavmeshLocation(50f);
                }
                currentState = State.Move;
                break;
            case State.Patrol:
             mesh.materials[0].color = Color.green;
                target = Checkpoint.transform.position;
                Checkpoint = Checkpoint.GetComponent<PatrolPointScript>().Next;
                currentState = State.Move;
                break;
            case State.Chase:
                mesh.materials[0].color = Color.red;
                agent.speed = 5f;
                
                break;
            case State.Return:
                // return
                break;
        }
        agent.SetDestination(target);
        //Debug.Log(currentState);
    }

    //function from internet: user Selzier on Unity forums
    public Vector3 RandomNavmeshLocation(float radius) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }
}
