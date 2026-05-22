using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 PlayerMovementVector;
    public float MoveSpeed;
    public float TurnSpeed;

    [SerializeField] private GameObject PlayerCam;
    void Start()
    {
        PlayerMovementVector = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddRelativeForce(MoveSpeed * PlayerMovementVector);
    }

    void OnMove(InputValue inputValue)
    {
        Vector2 MoveVector = inputValue.Get<Vector2>();
        Debug.Log(MoveVector);
        PlayerMovementVector = new Vector3(MoveVector.x, 0, MoveVector.y);
    }

    void OnLook(InputValue inputValue)
    {
        Vector2 LookVector = inputValue.Get<Vector2>();
        //Debug.Log(LookVector);
        transform.Rotate(0,LookVector.x * TurnSpeed,0);
        
        float VerticalTransform = PlayerCam.transform.eulerAngles.x - LookVector.y * TurnSpeed;
        Debug.Log(VerticalTransform);
        if (VerticalTransform > 90 && 180 >= VerticalTransform)
        {
            //PlayerCam.transform.Rotate(90 - PlayerCam.transform.eulerAngles.x,0,0);
        }
        else if (VerticalTransform < 270 && VerticalTransform > 180)
        {
            //PlayerCam.transform.Rotate(270 - PlayerCam.transform.eulerAngles.x,0,0);
        }
        else
        {
            PlayerCam.transform.Rotate(-LookVector.y * TurnSpeed,0,0);
        }
    }
}
