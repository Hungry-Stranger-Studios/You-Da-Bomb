using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PressurePuzzleLogic : MonoBehaviour
{
    public Image PressureBar;
    public float Pressure, MaxPressure;
    public float decAmount;
    public float ChargeRate;
    public float StartTime;


    public void Awake()
    {
        StartTime = Time.time;
    }

    public void Update()
    {
        if (Time.time - StartTime > .5f)
        {
            IncreasePressure();
            StartTime = Time.time;
        }
    }
    public void DecreasePressure()
    {
        Debug.Log("Pressure Decreased");
        Pressure -= decAmount;
        Debug.Log(Pressure);

        if( Pressure < 0 )
        {
            Pressure = 0;
        }
        PressureBar.fillAmount = Pressure / MaxPressure;

    }

    private void IncreasePressure()
    {
 
            Pressure += ChargeRate / 10f;
            if(Pressure > MaxPressure)
            {
                Pressure = MaxPressure;
                Debug.Log("Game Over");
            }
            PressureBar.fillAmount = Pressure / MaxPressure;       
    }

    
}
