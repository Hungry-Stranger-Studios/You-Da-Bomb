using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<DragDrop> draggableObject;
    public float snapRange = 0.5f;

    public bool isCorrectlySnapped = false; //new line

    private void Update()
    {

    }
    void Start()
    {
        foreach (DragDrop draggable in draggableObject)
        {
            draggable.dragEndedCallback = OnDragEnded;
        }

    }

    private void OnDragEnded(DragDrop draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        foreach (Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.position, snapPoint.position);
            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange)
        {
            draggable.transform.position = closestSnapPoint.position;
        }

    }

}
