using UnityEngine;

public class GuardController : MonoBehaviour
{

    public enum State { Patrol, Chase, Return}
    public State currentState = State.Patrol;

    public Vector3 playerLastKnownLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                // patrol
                break;
            case State.Chase:
                // chase
                break;
            case State.Return:
                // return
                break;
        }
    }
}
