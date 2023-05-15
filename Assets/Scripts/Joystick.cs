using UnityEngine;

public class Joystick : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private RectTransform joystickOutline;
    [SerializeField] private RectTransform joystickCircle;

    [Header("Settings")]
    [SerializeField] private float moveFactor;
    private Vector3 move;
    private Vector3 clickedPosition;
    private bool canControl;

    public static Joystick Instance { get => instance; }
    private static Joystick instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        HideJoystick();
    }

    private void Update()
    {
        if (canControl)
        {
            ControlJoystick();
        }
    }

    public void ClickJoystick()
    {
        clickedPosition = Input.mousePosition;
        joystickOutline.position = clickedPosition;

        ShowJoystick();
    }

    public Vector3 GetMoveVector()
    {
        return move;
    }

    private void ControlJoystick()
    {
        Vector3 currentPosition = Input.mousePosition;
        Vector3 direction = currentPosition - clickedPosition;

        float moveMagnitude = direction.magnitude * moveFactor / Screen.width;
        moveMagnitude = Mathf.Min(moveMagnitude, joystickOutline.rect.width / 2);
        move = direction.normalized * moveMagnitude;
        Vector3 targetPosition = clickedPosition + move;

        joystickCircle.position = targetPosition;

        if (Input.GetMouseButtonUp(0))
        {
            HideJoystick();
        }
    }

    private void ShowJoystick()
    {
        joystickOutline.gameObject.SetActive(true);
        canControl = true;
    }

    private void HideJoystick()
    {
        joystickOutline.gameObject.SetActive(false);
        canControl = false;

        move = Vector3.zero;
    }

}
