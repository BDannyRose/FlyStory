using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance { get; private set; }

    [SerializeField]
    private Slider slider;

    void Awake()
    {
        instance = this;
    }


    public void SetMaxHealth (int value)
    {
        slider.maxValue = value;
    }

    public void SetHealth (int value)
    {
        slider.value = value;
    }
}
