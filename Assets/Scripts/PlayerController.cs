using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Vector3 PlayerMovementVector;
    public float MoveSpeed;
    public float TurnSpeed;
    public bool IsCrouched;

    public Transform soundSphere;
    public float minSoundSphereRadius;
    public float maxSoundSphereRadius;
    private float scaleSoundSphere;
    private bool dead;

    [SerializeField] private GameObject PlayerCam;
    [SerializeField] private float CrouchDiff;
    [SerializeField] private float HeightDiff;
    [SerializeField] private GameObject text;
    void Start()
    {
        PlayerMovementVector = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsCrouched = false;
        dead = false;
        text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // movement of player
        PlayerMovementVector.Normalize();
        GetComponent<Rigidbody>().AddRelativeForce(MoveSpeed * PlayerMovementVector);

        // scale the sound sphere as player moves - faster means player makes more sound
       // scaleSoundSphere = minSoundSphereRadius + Vector3.Magnitude(GetComponent<Rigidbody>().linearVelocity) * (maxSoundSphereRadius - minSoundSphereRadius) / MoveSpeed;
        //soundSphere.transform.localScale = new Vector3(scaleSoundSphere, scaleSoundSphere, scaleSoundSphere);
    }

    private void FixedUpdate()
    {
    }

    void OnMove(InputValue inputValue)
    {
        if (dead) {return;}
        Vector2 MoveVector = inputValue.Get<Vector2>();
        //Debug.Log(MoveVector);
        PlayerMovementVector = new Vector3(MoveVector.x, 0, MoveVector.y);
    }

    
    void OnLook(InputValue inputValue)
    {
        if (dead) {return;}
        Vector2 LookVector = inputValue.Get<Vector2>();
        //Debug.Log(LookVector);
        transform.Rotate(0,LookVector.x * TurnSpeed,0);
        
        float VerticalTransform = PlayerCam.transform.eulerAngles.x - LookVector.y * TurnSpeed;
        //Debug.Log(VerticalTransform);
        /*
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
        */
    }
    

    void OnCrouch(InputValue action)
    {
        if (dead) {return;}
        if (!IsCrouched)
        {
            transform.Translate(0,-CrouchDiff,0);
            PlayerCam.transform.Translate(0,HeightDiff,-1.721f);
            transform.localScale += new Vector3(0f,-CrouchDiff,0f);
            MoveSpeed = 0;
            
            
        }
        else
        {
            transform.localScale += new Vector3(0f,CrouchDiff,0f);
            transform.Translate(0,CrouchDiff,0);
            PlayerCam.transform.Translate(0f,-HeightDiff,1.721f);
            MoveSpeed = 20;
            
            
            
        }
        IsCrouched = !IsCrouched;
    }


    void Die()
    {
        dead = true;
        transform.Rotate(90,0,0);
        MoveSpeed = 0;
        text.SetActive(true);
    }
}
