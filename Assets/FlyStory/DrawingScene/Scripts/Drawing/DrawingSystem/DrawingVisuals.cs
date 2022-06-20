using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingVisuals : MonoBehaviour
{
    private Grid<DrawingSystem.GridObject> grid;
    private Mesh mesh;
    private bool isInitialized = false;
    private int brushSize = 1;

    private void Awake()
    {
        //mesh = new Mesh();
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(Grid<DrawingSystem.GridObject> grid)
    {
        this.grid = grid;
        isInitialized = true;
        grid.OnGridValueChanged += Grid_OnGridValueChanged;
    }
    private void Grid_OnGridValueChanged(object sender, Grid<DrawingSystem.GridObject>.OnGridValueChangedEventsArgs e)
    {
        UpdateQuadRange(e.x, e.y);
    }

    public void StopUpdating()
    {
        grid.OnGridValueChanged -= Grid_OnGridValueChanged;
    }

    private void UpdateQuadRange(int originX, int originY)
    {
        Vector3 quadSize = new Vector3(1, 1) * grid.GetCellSize();
        if (!isInitialized)
        {
            quadSize *= 0f;
        }
        DrawingSystem.GridObject gridObject;
        Vector2 gridUV00, gridUV11;
        gridObject = grid.GetGridObject(originX, originY);
        gridUV00 = gridObject.GetColorUV();
        gridUV11 = gridObject.GetColorUV();
        MeshUtils.AddToMesh(mesh, grid.GetWorldPosition(originX, originY) + quadSize * 0.5f, 0f, quadSize * brushSize, gridUV00, gridUV11);
    }

    public void SetBrushSize(int bSize)
    {
        brushSize = bSize;
    }
}
