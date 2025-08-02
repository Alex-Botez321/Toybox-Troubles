using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SensSliderPrefs : MonoBehaviour
{
    private Slider sensSlider;

    private void Awake()
    {
        sensSlider = GetComponent<Slider>();

        if (PlayerPrefs.GetFloat("sensitivity") > sensSlider.minValue)
            sensSlider.value = PlayerPrefs.GetFloat("sensitivity");
    }

    public void OnSensChange()
    {
        PlayerPrefs.SetFloat("sensitivity", sensSlider.value);
    }
}
