using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

public class PlayerController : MonoBehaviour
{
    private Vector3 PlayerMovementVector;
    public float MoveSpeed;
    public float TurnSpeed;
    public bool IsCrouched;
    public int InLight;
    public Transform soundSphere;
    public float minSoundSphereRadius;
    public float maxSoundSphereRadius;
    private float scaleSoundSphere;
    private bool dead;
    private float TrueMoveSpeed;
    [SerializeField] private GameObject PlayerCam;
    [SerializeField] private float CrouchDiff;
    [SerializeField] private float HeightDiff;
    [SerializeField] private GameObject text;
    [SerializeField] private MeshRenderer playerMeshRenderer;
    [SerializeField] private GameObject disguiseText;
    private Color originalColor;
    private bool isDisguised = false;
    private float disguiseTimer = 0f;

    public event Action OnWin;
    public event Action OnLose;

    void Start()
    {
        PlayerMovementVector = Vector3.zero;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsCrouched = false;
        dead = false;
        if (text != null) text.SetActive(false);
        if (disguiseText != null) disguiseText.SetActive(false);
        TrueMoveSpeed = MoveSpeed;
        InLight = 0;
        if (playerMeshRenderer != null)
            originalColor = playerMeshRenderer.material.color;
        MusicScript.Instance.ChangeMusic(MusicScript.Instance.gameTheme);
    }

    void Update()
    {
        if (isDisguised)
        {
            disguiseTimer -= Time.deltaTime;
            if (disguiseTimer <= 0f)
            {
                isDisguised = false;
                if (playerMeshRenderer != null)
                    playerMeshRenderer.material.color = originalColor;
                if (disguiseText != null)
                    disguiseText.SetActive(false);
            }
        }
    }

    void FixedUpdate()
    {
        PlayerMovementVector.Normalize();
        GetComponent<Rigidbody>().AddRelativeForce(TrueMoveSpeed * PlayerMovementVector);
    }

    void OnMove(InputValue inputValue)
    {
        if (dead) return;
        Vector2 MoveVector = inputValue.Get<Vector2>();
        PlayerMovementVector = new Vector3(MoveVector.x, 0, MoveVector.y);
    }

    void OnLook(InputValue inputValue)
    {
        if (dead) return;
        Vector2 LookVector = inputValue.Get<Vector2>();
        transform.Rotate(0, LookVector.x * TurnSpeed, 0);
        float VerticalTransform = PlayerCam.transform.eulerAngles.x - LookVector.y * TurnSpeed;
    }

    void OnCrouch(InputValue action)
    {
        if (dead) return;
        if (!IsCrouched)
        {
            transform.Translate(0, -CrouchDiff, 0);
            PlayerCam.transform.Translate(0, HeightDiff, -1.721f);
            transform.localScale += new Vector3(0f, -CrouchDiff, 0f);
            TrueMoveSpeed = 0;
        }
        else
        {
            transform.localScale += new Vector3(0f, CrouchDiff, 0f);
            transform.Translate(0, CrouchDiff, 0);
            PlayerCam.transform.Translate(0f, -HeightDiff, 1.721f);
            TrueMoveSpeed = MoveSpeed;
        }
        IsCrouched = !IsCrouched;
    }

    public void ActivateDisguise(float duration)
    {
        isDisguised = true;
        disguiseTimer = duration;
        if (playerMeshRenderer != null)
            playerMeshRenderer.material.color = Color.green;
        if (disguiseText != null)
            disguiseText.SetActive(true);
    }

    public bool IsDisguised()
    {
        return isDisguised;
    }

    public void Die()
    {
        dead = true;
        transform.Rotate(90, 0, 0);
        TrueMoveSpeed = 0;
        if (text != null) text.SetActive(true);
        OnLose?.Invoke();
    }

    public void Win()
    {
        if (text != null)
        {
            text.SetActive(true);
            text.GetComponent<TextMeshProUGUI>().text = "You Win!";
        }
        OnWin?.Invoke();
    }

    public void EnterLight()
    {
        InLight++;
    }

    public void ExitLight()
    {
        InLight--;
    }
}