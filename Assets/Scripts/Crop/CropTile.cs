using UnityEngine;

public class CropTile : MonoBehaviour
{
    public enum State { Empty, Sown, Watered }
    private State state;

    [Header("Elements")]
    [SerializeField] private Transform cropParent;

    private void Start()
    {
        state = State.Empty;
    }

    private void Update()
    {

    }

    public bool IsEmpty()
    {
        return state == State.Empty;
    }

    public void Sow(CropData cropData)
    {
        state = State.Sown;
        Crop crop = Instantiate(cropData.cropPrefab, transform.position, Quaternion.identity, cropParent);

    }
}
