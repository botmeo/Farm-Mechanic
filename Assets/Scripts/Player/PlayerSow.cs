﻿using UnityEngine;

[RequireComponent(typeof(PlayerAnimations))]
[RequireComponent(typeof(PlayerToolSelector))]

public class PlayerSow : MonoBehaviour
{
    [Header("Elemenst")]
    private PlayerAnimations playerAnimations;
    private PlayerToolSelector playerTool;
    [SerializeField] private GameObject toolSow;

    [Header("Settings")]
    private CropField currentCropField;

    private void Start()
    {
        playerAnimations = GetComponent<PlayerAnimations>();
        playerTool = GetComponent<PlayerToolSelector>();

        SeedParticle.onSeedCollision += SeedCollisionCallback;
        CropField.onFullySown += CropFieldFullySownCallback;

        playerTool.onToolSelected += ToolSelectedCallback;
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        SeedParticle.onSeedCollision -= SeedCollisionCallback;
        CropField.onFullySown -= CropFieldFullySownCallback;

        playerTool.onToolSelected -= ToolSelectedCallback;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsEmpty())
        {
            toolSow.SetActive(true);
            currentCropField = other.GetComponent<CropField>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CropField") && other.GetComponent<CropField>().IsEmpty())
        {
            EnteredCropField(other.GetComponent<CropField>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CropField"))
        {
            toolSow.SetActive(false);
            playerAnimations.StopSowAnimation();
            currentCropField = null;
            playerTool.SelectTool(0);
        }
    }

    private void EnteredCropField(CropField cropField)
    {
        if (playerTool.CanSow())
        {
            playerAnimations.PlaySowAnimation();
        }
    }

    private void SeedCollisionCallback(Vector3[] seedPosition)
    {
        if (currentCropField == null)
        {
            return;
        }

        currentCropField.SeedCollisionCallback(seedPosition);
    }

    private void CropFieldFullySownCallback(CropField cropField)
    {
        if (cropField == currentCropField)
        {
            playerAnimations.StopSowAnimation();
        }
    }

    private void ToolSelectedCallback(PlayerToolSelector.Tool selectedTool)
    {
        if (!playerTool.CanSow())
        {
            playerAnimations.StopSowAnimation();
        }
    }
}
