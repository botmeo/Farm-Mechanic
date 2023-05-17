using System;
using UnityEngine;

public class PlayerToolSelector : MonoBehaviour
{
    public enum Tool { None, Sow, Watering, Harvest }
    private Tool activeTool;

    [Header("Actions")]
    public Action<Tool> onToolSelected;

    private void Start()
    {
        SelectTool(0);
    }

    private void Update()
    {

    }

    public void SelectTool(int toolIndex)
    {
        activeTool = (Tool)toolIndex;
        onToolSelected?.Invoke(activeTool);
    }

    public bool NoneCan()
    {
        return activeTool == Tool.None;
    }

    public bool CanSow()
    {
        return activeTool == Tool.Sow;
    }

    public bool CanWatering()
    {
        return activeTool == Tool.Watering;
    }

    public bool CanHarvest()
    {
        return activeTool == Tool.Harvest;
    }
}
