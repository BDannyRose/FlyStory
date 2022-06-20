using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentColor : MonoBehaviour
{
    private Mesh mesh;
    public Vector2 colorUV = Vector2.zero;
    private Vector2[] uvs = new Vector2[4];

    private void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        ChangeColor(colorUV);
    }

    public void ChangeColor(Vector2 colorUV)
    {
        Debug.Log(colorUV);

        uvs[0] = colorUV;
        uvs[1] = colorUV;
        uvs[2] = colorUV;
        uvs[3] = colorUV;

        mesh.uv = uvs;
    }
}
