using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 0.5f;
    private Material material;
    private Vector2 offset;
    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = material.mainTextureOffset;
    }

    void Update()
    {
        offset.x += scrollSpeed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }
}
