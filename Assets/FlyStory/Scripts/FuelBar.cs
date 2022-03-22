using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    public static FuelBar instance { get; private set; }

    [SerializeField]
    private Slider slider;


    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        slider.value = PlaneController.fuel;
    }

    public void SetMaxFuel(float value)
    {
        slider.maxValue = value;
    }

    public void SetFuel(float value)
    {
        slider.value = value;
    }
}
