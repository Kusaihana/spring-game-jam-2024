using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private TMP_Text _flashlightText;
    
    public bool isFlashlightOn;

    private float _flashLightIntensity;
    private Light _flashLight;

    private const float StartingIntensity = 5;
    private const float IntensityDecayRate = 0.1f;

    void Awake()
    {
        _flashLightIntensity = StartingIntensity;
        _flashLight = GetComponent<Light>();
    }

    void Update()
    {
        if (_flashLightIntensity > 0f && isFlashlightOn)
        {
            _flashLightIntensity -= IntensityDecayRate * Time.deltaTime;
            _flashLight.intensity = _flashLightIntensity;
        }
        else
        {
            _flashLight.intensity = 0f;
        }
        
        if (isFlashlightOn)
        {
            var flashlightBattery = (int)(100 * _flashLightIntensity / StartingIntensity);
            _flashlightText.text = $"Flashlight Battery: {flashlightBattery}%";
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            isFlashlightOn = !isFlashlightOn;
            
            if(!isFlashlightOn)
            {
                _flashlightText.text = "Flashlight: OFF";
            }

            _flashLight.intensity = isFlashlightOn ? _flashLightIntensity : 0;
        }
    }

    public void AddLightIntensity(float intensity)
    {
        var newIntensity = _flashLight.intensity + intensity;
        newIntensity = Math.Clamp(newIntensity, 0, StartingIntensity);
        _flashLightIntensity = newIntensity;
        _flashLight.intensity = _flashLightIntensity;
    }
}
