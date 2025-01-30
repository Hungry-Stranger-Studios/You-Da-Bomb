using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PressurePuzzleLogic : PuzzleBase
{
    public Image PressureBar;
    public float Pressure, MaxPressure;
    public float decAmount;
    public float ChargeRate;
    public float StartTime;


    public void Awake()
    {
        StartTime = Time.time;

        //Puzzle Base
        puzzleName = "Pressure Bar";
        puzzleGridHeight = 1;
        puzzleGridWidth = 1;
        puzzleType = "Constant";

        Activate();
    }

    public override void Activate()
    {
        base.Activate();
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
        Pressure -= decAmount;

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
                StressManagement.Instance.AdjustStress(100.0f);
            }
            PressureBar.fillAmount = Pressure / MaxPressure;       
    }

    
}
