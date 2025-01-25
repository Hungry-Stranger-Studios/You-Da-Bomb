using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnableDial : MonoBehaviour
{
    private float currentFrequency; //Current frequency from dial
    private bool isDragging = false; //If user is using dial
    private Vector3 dialCenter; //Center of dial in world space
    private float previousAngle;

    void Start()
    {
        dialCenter = this.transform.position;
    }

    void Update()
    {
        HandleInput();

        //Map dial's rotation to a frequency range
        float rotation = NormalizeRotation(this.transform.localEulerAngles.z);
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) //Started turning
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            float distanceToCenter = Vector3.Distance(mousePosition, dialCenter);

            //Allow dragging if cursor near dial
            if (distanceToCenter <= 0.5f)
            {
                isDragging = true;
                previousAngle = GetAngleFromCenter(mousePosition);
            }
        }

        if (Input.GetMouseButtonUp(0)) //Stopped turning
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 mousePosition = GetMouseWorldPosition();
            float currentAngle = GetAngleFromCenter(mousePosition);

            //Rotate dial
            float angleDelta = currentAngle - previousAngle;
            this.transform.Rotate(0, 0, angleDelta);

            previousAngle = currentAngle; //Update previous angle
        }
    }

    //Get angle of mouse from dial (For rotation)
    private float GetAngleFromCenter(Vector3 mousePosition)
    {
        Vector3 direction = mousePosition - dialCenter;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; //rads to degrees
    }

    //Get position of mouse (For rotation)
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Camera.main.WorldToScreenPoint(dialCenter).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }

    //Normalize rotation angle to 0 --> 360
    private float NormalizeRotation(float angle)
    {
        while (angle < 0) angle += 360f;
        while (angle > 360) angle -= 360f;
        return angle;
    }
}
