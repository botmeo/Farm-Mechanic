using UnityEngine;

[RequireComponent(typeof(PlayerAnimations))]
[RequireComponent(typeof(PlayerToolSelector))]

public class PlayerWater : MonoBehaviour
{
    [Header("Elemenst")]
    private PlayerAnimations playerAnimations;
    private PlayerToolSelector playerTool;
    [SerializeField] private GameObject toolWatering;

    [Header("Settings")]
    private CropField currentCropField;

    private void Start()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        playerTool = GetComponent<PlayerToolSelector>();

        WaterParticle.onWaterCollision += WaterCollisionCallback;
        CropField.onFullyWater -= CropFieldFullyWateredCallback;

        playerTool.onToolSelected += ToolSelectedCallback;
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        WaterParticle.onWaterCollision -= WaterCollisionCallback;
        CropField.onFullyWater -= CropFieldFullyWateredCallback;

        playerTool.onToolSelected -= ToolSelectedCallback;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsSown())
        {
            toolWatering.SetActive(true);
            currentCropField = other.GetComponent<CropField>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            EnteredCropField(other.GetComponent<CropField>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            toolWatering.SetActive(false);
            playerAnimations.StopWateringAnimation();
            currentCropField = null;
            playerTool.SelectTool(0);
        }
    }

    private void EnteredCropField(CropField cropField)
    {
        if (playerTool.CanWatering())
        {
            playerAnimations.PlayWateringAnimation();
        }
    }

    private void WaterCollisionCallback(Vector3[] waterPosition)
    {
        if (currentCropField == null)
        {
            return;
        }

        currentCropField.WaterCollisionCallback(waterPosition);
    }

    private void CropFieldFullyWateredCallback(CropField cropField)
    {
        if (cropField == currentCropField)
        {
            playerAnimations.StopWateringAnimation();
        }
    }

    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!playerTool.CanWatering())
        {
            playerAnimations.StopWateringAnimation();
        }
    }
}
