using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoldHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private float holdBuffer = 0.2f;

    private int holdThreshold;
    private float holdDuration = 0f;
    private bool isHeld = false;
    private float currentHoldTime;

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
            currentHoldTime = holdDuration; //Update static for access in HoldButtonPuzzle
        }
    }

    public float getHoldThreshold() { return holdThreshold; }
    public float getCurrentHoldTime() { return currentHoldTime; }
    public float getHoldBuffer() { return holdBuffer; }
    public void setHoldBuffer(float holdBuffer) { this.holdBuffer = holdBuffer; }
    public void setCurrentHoldTime(float currentHoldTime) { this.currentHoldTime = currentHoldTime; }
    public void setHoldThreshold(int holdThreshold) { this.holdThreshold = holdThreshold; }
}
