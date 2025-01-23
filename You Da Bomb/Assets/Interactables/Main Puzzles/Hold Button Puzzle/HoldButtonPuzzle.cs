using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoldButtonPuzzle : PuzzleBase
{
    [Header("Prefabs")]
    [SerializeField] private GameObject buttonPrefab;

    [Header("Button Stats")]
    [SerializeField] private float holdBuffer = 0.2f;

    private Canvas puzzleCanvas;

    private void Awake()
    {
        //PuzzleBase
        puzzleName = "Hold Button";
        puzzleGridHeight = 1; 
        puzzleGridWidth = 1;
        puzzleType = "Main";

        //Make canvas
        GameObject canvasGO = new GameObject("PuzzleCanvas");
        puzzleCanvas = canvasGO.AddComponent<Canvas>();
        canvasGO.transform.position = this.transform.position;
        canvasGO.transform.SetParent(this.transform);
        puzzleCanvas.renderMode = RenderMode.WorldSpace;

        //Canvas size and scaling
        RectTransform canvasRect = puzzleCanvas.GetComponent<RectTransform>();
        canvasRect.localScale = new Vector3(0.025f, 0.025f, 1f); //Scale canvas to puzzle size
        canvasRect.sizeDelta = new Vector2(puzzleGridWidth * 100f, puzzleGridHeight * 100f); //Set the size based on puzzle size
        canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.AddComponent<GraphicRaycaster>();

        //Make button
        GameObject buttonObject = Instantiate(buttonPrefab, puzzleCanvas.transform);
        ButtonHoldHandler buttonHandler = buttonObject.AddComponent<ButtonHoldHandler>();
        buttonHandler.holdBuffer = holdBuffer;

        //Adjust button position
        RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();
        buttonRect.anchoredPosition = new Vector2(0f, -10f);

        //Subscribe to button event
        buttonHandler.OnHoldComplete += OnHoldComplete;

        Activate();
    }

    public override void Activate()
    {
        base.Activate();
    }

    private void Update()
    {
        /*
        if (currentTimeText != null)
        {
            //currentTimeText.text = ButtonHoldHandler.currentHoldTime.ToString();
        }
        */
    }

    private void OnHoldComplete()
    {
        Solved();
    }

    public override void Solved()
    {
        base.Solved();
        Destroy(this.gameObject);
    }
}
