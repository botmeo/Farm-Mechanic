using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;

    [Header("Settings")]
    [SerializeField] private float moveSpeedMultiplier;

    private static PlayerAnimations instance;
    public static PlayerAnimations Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        Vector3 moveVector = PlayerController.Instance.moveVector;
        if (moveVector.magnitude > 0)
        {
            animator.transform.forward = moveVector.normalized;
            animator.SetFloat("MoveSpeed", moveVector.magnitude * moveSpeedMultiplier);
            animator.Play("Walking");
        }
        else
        {
            animator.Play("Idle");
        }
    }

    #region 

    #endregion

    #region Sow 

    public void PlaySowAnimation()
    {
        animator.SetLayerWeight(1, 1);
    }

    public void StopSowAnimation()
    {
        animator.SetLayerWeight(1, 0);
    }

    #endregion
}
