using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]

public class ClearScreenDraggable : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private bool isOutsideScreen = false;

    private ClearScreenPuzzle parentPuzzle;
    public void Initialize(ClearScreenPuzzle puzzle)
    {
        parentPuzzle = puzzle; //Assign parent puzzle
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));

    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;

        //Check if the object is outside screen bounds
        RectTransform parentRect = parentPuzzle.GetComponent<RectTransform>();
        if (!RectTransformUtility.RectangleContainsScreenPoint(parentRect, Camera.main.WorldToScreenPoint(transform.position), Camera.main))
        {
            if (!isOutsideScreen) 
            {
                isOutsideScreen = true;
                parentPuzzle.NotifyObjectDraggedOutside();
                Destroy(gameObject); //Destroy debris after its dragged out
            }
        }
    }
}
