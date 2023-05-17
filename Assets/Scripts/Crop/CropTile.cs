using UnityEngine;

public enum TileFieldState { Empty, Sown, Watered }

public class CropTile : MonoBehaviour
{
    private TileFieldState state;

    [Header("Elements")]
    [SerializeField] private Transform cropParent;

    private void Start()
    {
        state = TileFieldState.Empty;
    }

    private void Update()
    {

    }

    public bool IsEmpty()
    {
        return state == TileFieldState.Empty;
    }

    public bool IsSown()
    {
        return state == TileFieldState.Sown;
    }

    public void Sow(CropData cropData)
    {
        state = TileFieldState.Sown;
        Crop crop = Instantiate(cropData.cropPrefab, transform.position, Quaternion.identity, cropParent);
    }

    public void Water()
    {
        state = TileFieldState.Watered;
    }
}
