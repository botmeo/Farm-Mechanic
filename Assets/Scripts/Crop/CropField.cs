using System.Collections.Generic;
using UnityEngine;

public class CropField : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform tilesParent;
    private List<CropTile> cropTiles = new List<CropTile>();

    [Header("Settings")]
    [SerializeField] private CropData cropData;
    [SerializeField] private float minDistance;


    private void Start()
    {
        StoreTiles();
    }

    private void Update()
    {

    }

    private void StoreTiles()
    {
        for (int i = 0; i < tilesParent.childCount; i++)
        {
            cropTiles.Add(tilesParent.GetChild(i).GetComponent<CropTile>());
        }
    }

    public void SeedCollisionCallback(Vector3[] seedPosition)
    {
        for (int i = 0; i < seedPosition.Length; i++)
        {
            CropTile closetTile = GetClosetCropTile(seedPosition[i]);

            if (closetTile == null)
            {
                continue;
            }

            if (!closetTile.IsEmpty())
            {
                continue;
            }

            Sow(closetTile);
        }
    }

    private CropTile GetClosetCropTile(Vector3 seedPosition)
    {
        int closestCropTileIndex = -1;

        for (int i = 0; i < cropTiles.Count; i++)
        {
            CropTile cropTile = cropTiles[i];
            float distanceTileSeed = Vector3.Distance(cropTile.transform.position, seedPosition);

            if (distanceTileSeed < minDistance)
            {
                minDistance = distanceTileSeed;
                closestCropTileIndex = i;
            }
        }

        if (closestCropTileIndex == -1)
        {
            return null;
        }

        return cropTiles[closestCropTileIndex];
    }

    private void Sow(CropTile cropTile)
    {
        cropTile.Sow(cropData);
        /*tileSown++;

        if (tileSown == cropTiles.Count)
        {
            FieldFullySown();
        }*/
    }

    private void FieldFullySown()
    {

    }

}
