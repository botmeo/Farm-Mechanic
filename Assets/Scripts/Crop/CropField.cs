using System;
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
    private TileFieldState state;

    private int tileSown;
    private int tileWatered;

    [Header("Actions")]
    public static Action<CropField> onFullySown;
    public static Action<CropField> onFullyWater;

    private void Start()
    {
        state = TileFieldState.Empty;
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

    #region Sow

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

    private void Sow(CropTile cropTile)
    {
        cropTile.Sow(cropData);
        tileSown++;

        if (tileSown == cropTiles.Count)
        {
            FieldFullySown();
        }
    }

    private void FieldFullySown()
    {
        state = TileFieldState.Sown;
        onFullySown?.Invoke(this);
    }

    #endregion

    #region Water

    public void WaterCollisionCallback(Vector3[] waterPosition)
    {
        for (int i = 0; i < waterPosition.Length; i++)
        {
            CropTile closetTile = GetClosetCropTile(waterPosition[i]);

            if (closetTile == null)
            {
                continue;
            }

            if (!closetTile.IsSown())
            {
                continue;
            }

            Water(closetTile);
        }
    }

    private void Water(CropTile cropTile)
    {
        cropTile.Water();
        tileWatered++;
        if (tileWatered == cropTiles.Count)
        {
            FieldFullyWatered();
        }
    }

    private void FieldFullyWatered()
    {
        state = TileFieldState.Watered;
        onFullyWater?.Invoke(this);
    }

    #endregion

    public bool IsEmpty()
    {
        return state == TileFieldState.Empty;
    }

    public bool IsSown()
    {
        return state == TileFieldState.Sown;
    }

    public bool IsWatered()
    {
        return state == TileFieldState.Watered;
    }

}
