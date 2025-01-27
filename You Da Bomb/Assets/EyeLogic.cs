using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeLogic : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D boundary;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }
    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        mousePosition.z = transform.position.z;

        if (boundary != null)
        {
            Bounds bounds = boundary.bounds;

            //Keep cursor within bounds
            float clampedX = Mathf.Clamp(mousePosition.x, bounds.min.x, bounds.max.x);
            float clampedY = Mathf.Clamp(mousePosition.y, bounds.min.y, bounds.max.y);

            //Apply position to pupils
            transform.position = new Vector3(clampedX, clampedY, mousePosition.z);
        }
        else
        {
            //No boundary, follow mouse
            transform.position = mousePosition;
        }
    }
}
