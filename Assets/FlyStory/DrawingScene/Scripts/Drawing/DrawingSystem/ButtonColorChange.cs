using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorChange : MonoBehaviour
{
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void ChangeColor(bool isActive)
    {
        image.color = isActive ? Color.green : Color.red;
    }
}
