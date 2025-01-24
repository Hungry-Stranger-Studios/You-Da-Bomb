using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoldHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float holdBuffer = 0.2f;

    public int holdThreshold;
    public float holdDuration = 0f;
    private bool isHeld = false;
    //public static float currentHoldTime;

    public event System.Action OnHoldComplete;
    private void Awake()
    {
        holdThreshold = Random.Range(2, 4);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
        holdDuration = 0f; //Reset duration when the button is pressed
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
        if (holdDuration >= holdThreshold - holdBuffer && holdDuration <= holdThreshold + holdBuffer)
        {
            OnHoldComplete?.Invoke(); //Held for enough time
        }
        holdDuration = 0f;
    }

    void Update()
    {
        if (isHeld)
        {
            holdDuration += Time.deltaTime;
            //currentHoldTime = holdDuration; //Update static for access in HoldButtonPuzzle
        }
    }

    public float getHoldDuration() { return holdThreshold; }
}
