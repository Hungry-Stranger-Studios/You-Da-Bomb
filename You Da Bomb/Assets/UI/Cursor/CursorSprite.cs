using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSprite : MonoBehaviour
{
    private Camera mainCamera;
    private Animator animator;

    [Header("Cursor Follow Settings")]
    [SerializeField] private float followSmoothness = 10f;

    void Awake()
    {
        mainCamera = Camera.main;
        animator = GetComponent<Animator>();

        //Hide default cursor
        Cursor.visible = false;
    }

    void Update()
    {
        SmoothFollowCursor();

        if (Input.GetMouseButtonDown(0))
        {
            PlayClickAnimation();
        }

        if (Input.GetMouseButton(0))
        {
            HoldClickAnimation(true); // Freeze on 3rd sprite
        }
        else
        {
            HoldClickAnimation(false); // Resume animation
        }
    }

    private void SmoothFollowCursor()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; //Distance from camera
        Vector3 targetPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        transform.position = Vector3.Lerp(transform.position, targetPosition, followSmoothness * Time.deltaTime);
    }

    private void PlayClickAnimation()
    {
        animator.SetTrigger("Click");  
    }

    private void HoldClickAnimation(bool isHeld)
    {
        animator.SetBool("isHeld", isHeld);
    }

    public void EndClickAnimation()
    {
        animator.ResetTrigger("Click");
    }
}
