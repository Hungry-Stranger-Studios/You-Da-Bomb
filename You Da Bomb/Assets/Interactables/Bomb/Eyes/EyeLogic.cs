using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class EyeLogic : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D leftBoundary;
    [SerializeField] private CapsuleCollider2D rightBoundary;
    [SerializeField] private Transform leftPupil;
    [SerializeField] private Transform rightPupil;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        //Mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = mainCamera.ScreenPointToRay(mousePosition).origin;
        mousePosition.z = 0f;

        //Update each pupil
        if (leftBoundary != null && leftPupil != null)
        {
            MovePupilWithinBounds(leftPupil, leftBoundary, mousePosition);
        }

        if (rightBoundary != null && rightPupil != null)
        {
            MovePupilWithinBounds(rightPupil, rightBoundary, mousePosition);
        }
    }

    private void MovePupilWithinBounds(Transform pupil, CapsuleCollider2D boundary, Vector3 targetPosition)
    {
        //Get bounds
        Bounds bounds = boundary.bounds;

        //Keep pupils in bounds
        float clampedX = Mathf.Clamp(targetPosition.x, bounds.min.x, bounds.max.x);
        float clampedY = Mathf.Clamp(targetPosition.y, bounds.min.y, bounds.max.y);

        //Apply position from bounds
        pupil.position = new Vector3(clampedX, clampedY, pupil.position.z);
    }
}
