using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public Transform outline; // Reference the outline object
    public float baseResolution = 1920f; // Reference resolution
    public float baseScale = 1.05f; // Scale factor at reference resolution

    void Update()
    {
        float scaleFactor = Screen.width / baseResolution;
        outline.localScale = Vector3.one * baseScale * scaleFactor;
    }
}
