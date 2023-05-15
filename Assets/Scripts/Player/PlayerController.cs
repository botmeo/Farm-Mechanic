using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Joystick joystick;

    [Header("Settings")]
    [HideInInspector] public Vector3 moveVector;
    [SerializeField] private float moveSpeed;
    private CharacterController characterController;

    private static PlayerController instance;
    public static PlayerController Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        moveVector = joystick.GetMoveVector() * moveSpeed * Time.deltaTime / Screen.width;
        moveVector.z = moveVector.y;
        moveVector.y = 0;
        characterController.Move(moveVector);
    }
}
