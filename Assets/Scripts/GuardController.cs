using UnityEngine;
using UnityEngine.AI;

public class GuardController : MonoBehaviour
{

    public enum State { Patrol, Chase, Random, Move}
    public State currentState = State.Move;
    [SerializeField] private GameObject Player;

    public Vector3 target;

    public GameObject Checkpoint;
    public int randomness;

    private NavMeshAgent agent;
    private MeshRenderer mesh;
    public Vector3 LastKnownLocation;
    private Collider collider;
    public float viewAngle = 60f;
    public int rayCount = 60;
    public float viewDistance;

    [SerializeField] GameObject DetectionText;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         agent = GetComponent<NavMeshAgent>();
         target = transform.position;
         LastKnownLocation = transform.position;
         mesh = GetComponent<MeshRenderer>();
         collider = GetComponent<Collider>();

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
                target = RandomNavmeshLocation(50f);
                currentState = State.Move;
                break;
            case State.Patrol:
             mesh.materials[0].color = Color.green;
                target = Checkpoint.transform.position;
                Checkpoint = Checkpoint.GetComponent<PatrolPointScript>().Next;
                currentState = State.Move;
                break;
            case State.Chase:
                target = LastKnownLocation;
                mesh.materials[0].color = Color.red;
                agent.speed = 5f;
                if (Vector3.Distance(transform.position, target) <= 0.5)
                {
                    currentState = State.Move;
                    DetectionText.SetActive(false);
                }
                else
                {
                    DetectionText.SetActive(true);
                }
                break;
        }
        //search
        for (int i = 0; i < rayCount; i++)
        {
            float angle = (-viewAngle/2 + i * (viewAngle / rayCount));
            Vector3 dir = Quaternion.Euler(0, angle,0) * transform.forward * viewDistance;
            Ray r = new Ray(transform.position,dir);
            RaycastHit hit;

            if (Physics.Raycast(r, out hit, viewDistance))
            {
                if (hit.collider.gameObject == Player)
                {
                    if (!Player.GetComponent<PlayerController>().IsCrouched)
                    {
                        LastKnownLocation = Player.transform.position;
                        currentState = State.Chase;
                    }
                    
                }
            }
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Player)
        {
            other.gameObject.GetComponent<PlayerController>().Die();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
         for (int i = 0; i < rayCount; i++)
        {
            float angle = (-viewAngle/2 + i * (viewAngle / rayCount));
            Vector3 dir = Quaternion.Euler(0, angle,0) * transform.forward * viewDistance;
            Gizmos.DrawRay(transform.position, dir);
        }
    }

}
