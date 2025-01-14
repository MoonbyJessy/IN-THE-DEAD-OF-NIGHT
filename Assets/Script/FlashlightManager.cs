using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FlashlightState
{
    Off,
    On,
    Dead
}
public class FlashlightManager : MonoBehaviour
{

    [Range (0.0f, 2f)] [SerializeField] float batteryLossTick = 0.5f;

    [SerializeField] int startBattery = 100;

    public int currentBattery;

    public FlashlightState state;

    private bool flashlightIsOn;

    [SerializeField] KeyCode ToggleKey = KeyCode.F;

    [SerializeField] GameObject FlashlightLight;
    
    void Start()
    {
        currentBattery = startBattery;
        InvokeRepeating(nameof(LoseBattery), 0, batteryLossTick);
    }

    
    void Update()
    {
        if (Input.GetKeyUp(ToggleKey)) ToggleFlashlight();
        
        if (state == FlashlightState.Off) FlashlightLight.SetActive(false);
        else if (state == FlashlightState.On) FlashlightLight.SetActive(true);
        else if (state == FlashlightState.Dead) FlashlightLight.SetActive(false);

        if (currentBattery <= 0)
        {
            currentBattery = 0;
            state = FlashlightState.Dead;
            flashlightIsOn = false;
        }
    }
    public void GainBattery(int amount)
    {
        if (currentBattery ==  0)
        {
            state = FlashlightState.On;
            flashlightIsOn = true;
        }
        if ( currentBattery + amount > startBattery)
        {
            currentBattery = startBattery;
        }
        else 
        currentBattery += amount;
    }
    private void LoseBattery ()
    {
        if (state == FlashlightState.On) currentBattery--; 
    }
    private void ToggleFlashlight()
    {
        flashlightIsOn = !flashlightIsOn;
        if (state == FlashlightState.Dead) flashlightIsOn = false;

        if (flashlightIsOn)
        {
            state = FlashlightState.On;
        }
        else
        {
            state = FlashlightState.Off;
        }
    }
}
