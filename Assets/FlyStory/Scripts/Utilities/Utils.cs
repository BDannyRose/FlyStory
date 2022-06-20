using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public const int sortingOrderDefault = 5000;

    // создаёт на сцене текстовый объект
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3),
        int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, 
        TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }

    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, 
        TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    // возвращает положение мыши
    public static Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    public static int PickRandomItem(int randomNumber, int[] generationWeights)
    {
        int i;
        for (i = 0; i < generationWeights.Length; i++)
        {
            if (randomNumber < generationWeights[i])
            {
                return i;
            }
            else
            {
                randomNumber -= generationWeights[i];
            }
        }
        return -1;
    }
}
