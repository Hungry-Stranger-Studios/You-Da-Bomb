using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour
{
    public delegate void DragEndedDelegate(DragDrop draggableObject);

    public DragEndedDelegate dragEndedCallback;

    private bool dragging = false;
    private Vector3 offset;

    

    private void Update()
    {
        if (dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        
        
    }

    private void OnMouseUp()
    {
        dragging = false;
        dragEndedCallback(this);
        
    }
}
