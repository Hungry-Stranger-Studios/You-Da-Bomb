using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<DragDrop> draggableObject;
    public float snapRange = 0.5f;

    public bool isCorrectlySnapped = false; 

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
            draggable.transform.position = new Vector3(closestSnapPoint.position.x, closestSnapPoint.position.y, closestSnapPoint.position.z - 0.1f);
            isCorrectlySnapped = true;
            Debug.Log($"Snap Point: {closestSnapPoint.position} and Draggable: {draggable.transform.position}");
        }
        else
        {
            isCorrectlySnapped= false;
        }

    }

}
