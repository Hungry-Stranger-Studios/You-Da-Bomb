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

    private Coroutine recharge;


    public void Start()
    {
       
        PressureBar.fillAmount = 0;
        

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

        if(recharge != null)
        {
            StopCoroutine(recharge);
        }
        recharge = StartCoroutine(IncreasePressure());

    }

    private IEnumerator IncreasePressure()
    {
        yield return new WaitForSeconds(.5f);

        while(Pressure < MaxPressure)
        {
            Pressure += ChargeRate / 10f;
            if(Pressure > MaxPressure)
            {
                Pressure = MaxPressure;
            }
            PressureBar.fillAmount = Pressure / MaxPressure;
            yield return new WaitForSeconds(.1f);
        }
    }

    
}
