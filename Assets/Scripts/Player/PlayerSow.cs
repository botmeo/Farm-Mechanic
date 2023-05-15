using UnityEngine;

public class PlayerSow : MonoBehaviour
{
    [Header("Settings")]
    private CropField cropField;

    private void Start()
    {
        SeedParticle.onSeedCollision += SeedCollisionCallback;
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        SeedParticle.onSeedCollision -= SeedCollisionCallback;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            PlayerAnimations.Instance.PlaySowAnimation();
            cropField = other.GetComponent<CropField>();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            PlayerAnimations.Instance.StopSowAnimation();
            cropField = null;
        }
    }

    private void SeedCollisionCallback(Vector3[] seedPosition)
    {
        if (cropField == null)
        {
            return;
        }

        cropField.SeedCollisionCallback(seedPosition);
    }
}
