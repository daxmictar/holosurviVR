using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class playerBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void UpdatehealthBar(int currentValue, int maxValue)
    {
        slider.value = currentValue / maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
