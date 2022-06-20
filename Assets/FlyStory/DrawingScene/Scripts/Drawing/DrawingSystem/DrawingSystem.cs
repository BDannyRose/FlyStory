using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class DrawingSystem : MonoBehaviour
{
    private enum BrushType
    {
        Thin = 4,
        Average = 8,
        Thick = 16,
    }
    private BrushType brushType = BrushType.Thin;
    public bool erasureActive = false;

    public ButtonColorChange brushThin, brushAverage, brushThick, erase;

    public DrawingVisuals drawingVisuals;
    public GameObject drawingVisualsGameObject;
    public Grid<GridObject> grid;
    public Vector2 colorUV = Vector2.zero;
    public Texture2D colorTexture2D;
    
    private GridObject currentGridObject;
    private GridObject lastGridObject;
    private int meshLimitCounter = 0;

    public RectTransform bottomLeft;
    public RectTransform upperRight;
    public Vector3 originPoint;
    public int width, height;


    private void Awake()
    {
        transform.position = bottomLeft.position + new Vector3(5, 5);
        originPoint = transform.position;
        width = (int)(upperRight.position.x - originPoint.x - 5);
        height = (int)(upperRight.position.y - originPoint.y - 5);

        brushThin.ChangeColor(true);
        brushAverage.ChangeColor(false);
        brushThick.ChangeColor(false);
        erase.ChangeColor(false);
    }

    private void Start()
    {
        grid = new Grid<GridObject>(width, height, 1f, originPoint, (Grid<GridObject> g, int x, int y) => new GridObject(g, x, y));
        drawingVisuals.SetGrid(grid);
        transform.position = Vector3.zero;
        drawingVisuals.transform.position = Vector3.zero;
        drawingVisuals.SetBrushSize((int)brushType);
    }

    private void Update()
    {
        if (meshLimitCounter > 45000)
        {
            drawingVisuals.StopUpdating();
            drawingVisuals = Instantiate(drawingVisualsGameObject, transform.position, Quaternion.identity, null).GetComponent<DrawingVisuals>();
            drawingVisuals.SetGrid(grid);
            drawingVisuals.transform.position = Vector3.zero;
            drawingVisuals.SetBrushSize((int)brushType);
            meshLimitCounter = 0;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 position = Utils.GetMousePosition();
            currentGridObject = grid.GetGridObject(position);
            if (currentGridObject != null && currentGridObject != lastGridObject)
            {
                currentGridObject.SetColorUVRange(colorUV, ((int)brushType), erasureActive);
                meshLimitCounter += 6;
            }
            lastGridObject = currentGridObject;
        }
    }

    public void SaveImage()
    {
        Texture2D texture2D = new Texture2D(grid.GetWidth(), grid.GetHeight(), TextureFormat.ARGB32, false);

        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                GridObject gridObject = grid.GetGridObject(x, y);

                int pixelX = (int)(gridObject.GetColorUV().x * colorTexture2D.width);
                int pixelY = (int)(gridObject.GetColorUV().y * colorTexture2D.height);
                Color pixelColor = colorTexture2D.GetPixel(pixelX, pixelY);
                pixelColor.a = gridObject.GetTransparency();

                texture2D.SetPixel(x, y, pixelColor);
            }
        }

        texture2D.Apply();
        byte[] byteArray = texture2D.EncodeToPNG();

        if (!Directory.Exists(Application.persistentDataPath + "/sprites/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/sprites/");
        }
        System.IO.File.WriteAllBytes(Application.persistentDataPath + "/sprites/" + "user.png", byteArray);
    }

    public void SetBrushTypeThin()
    {
        brushType = BrushType.Thin;
        drawingVisuals.SetBrushSize((int)brushType);
        brushThin.ChangeColor(true);
        brushAverage.ChangeColor(false);
        brushThick.ChangeColor(false);
    }
    public void SetBrushTypeAverage()
    {
        brushType = BrushType.Average;
        drawingVisuals.SetBrushSize((int)brushType);
        brushThin.ChangeColor(false);
        brushAverage.ChangeColor(true);
        brushThick.ChangeColor(false);
    }
    public void SetBrushTypeThick()
    {
        brushType = BrushType.Thick;
        drawingVisuals.SetBrushSize((int)brushType);
        brushThin.ChangeColor(false);
        brushAverage.ChangeColor(false);
        brushThick.ChangeColor(true);
    }
    public void SwitchErasure()
    {
        erasureActive = !erasureActive;
        colorUV = erasureActive ? Vector2.zero : colorUV;
        erase.ChangeColor(erasureActive);
    }
    


    public class GridObject
    {
        private Grid<GridObject> grid;
        private int x;
        private int y;
        private float colorA;
        private Vector2 colorUV;

        public GridObject(Grid<GridObject> grid, int x, int y)
        {
            this.grid = grid;
            this.x = x;
            this.y = y;
            this.colorA = 0f;
            colorUV = Vector2.zero;
        }

        public void SetColorUVRange(Vector2 colorUV, int range, bool erasureActive)
        {
            Debug.Log("drawingSystem");
            GridObject gridObject;
            for (int _x = 0; _x < range; _x++)
            {
                for (int _y = 0; _y < range; _y++)
                {
                    gridObject = grid.GetGridObject(x + _x, y + _y);
                    gridObject?.SetColorUV(colorUV, erasureActive);
                }
            }
            grid.TriggerGridObjectChanged(x, y);
        }

        public void SetColorUV(Vector2 colorUV, bool erasureActive)
        {
            this.colorUV = colorUV;
            this.colorA = erasureActive ? 0f : 1f;
        }

        public Vector2 GetColorUV()
        {
            return colorUV;
        }

        public float GetTransparency()
        {
            return colorA;
        }

        public override string ToString()
        {
            return colorUV.x.ToString();
        }
    }
}
