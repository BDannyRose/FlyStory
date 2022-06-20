using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    public DrawingSystem drawingSystem;
    public CurrentColor currentColor;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f))
            {
                Vector2 colovUV = raycastHit.textureCoord2;
                currentColor.ChangeColor(colovUV);
                drawingSystem.colorUV = raycastHit.textureCoord;
                if (drawingSystem.erasureActive) drawingSystem.SwitchErasure();
            }
        }
    }

}
