using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem waterParticles;

    [Header("Settings")]
    [SerializeField] private float moveSpeedMultiplier;

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

    #region Watering

    public void PlayWateringAnimation()
    {
        animator.SetLayerWeight(2, 1);
    }

    public void StopWateringAnimation()
    {
        animator.SetLayerWeight(2, 0);
        waterParticles.Stop();
    }

    #endregion

}
