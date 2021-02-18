﻿
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

public class Set_Int : UdonSharpBehaviour
{
    public UdonBehaviour udonBehaviour;
    public string variableName;
    public int value;
    public bool getOnStart = false;
    public int step = 1;

    public Text display;
    public Slider slider;

    private int originalValue;
    private bool sliderInitialize = false;

    void Start()
    {
        if (getOnStart)
        {
            if (udonBehaviour)
            {
                originalValue = (int)udonBehaviour.GetProgramVariable(variableName);
            }
            
            value = originalValue;
        }
        else
        {
            originalValue = value;
        }

        if (display)
        {
            display.text = value.ToString();
        }

        if (slider)
        {
            slider.wholeNumbers = true;
            slider.value = value;
            sliderInitialize = true;
        }

        if (0 > step)
        {
            step = Mathf.Abs(step);
        }
    }

    public void SetInt()
    {
        if (udonBehaviour)
        {
            udonBehaviour.SetProgramVariable(variableName, value);
        }

        if (slider)
        {
            if(slider.value != value)
            {
                sliderInitialize = false;
                slider.value = value;
                sliderInitialize = true;
            }
        }

        if (display)
        {
            display.text = value.ToString();
        }
    }

    public void Increase()
    {
        value += step;
        SetInt();
    }

    public void Decrease()
    {
        value -= step;
        SetInt();
    }

    public void ResetValue()
    {
        value = originalValue;
        SetInt();
    }

    public void SetSlider()
    {
        if (slider && sliderInitialize)
        {
            value = Mathf.RoundToInt(slider.value);
            SetInt();
        }
    }
}
