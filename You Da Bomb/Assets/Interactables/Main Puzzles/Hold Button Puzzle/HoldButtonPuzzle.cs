using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HoldButtonPuzzle : PuzzleBase
{
    [Header("Prefabs")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject textPrefab;

    [Header("Button Stats")]
    [SerializeField] private float holdBuffer = 0.2f;

    private Canvas puzzleCanvas;
    private GameObject textDurationObject;
    private GameObject textCurrentObject;
    private ButtonHoldHandler buttonHandler;

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
        buttonHandler = buttonObject.AddComponent<ButtonHoldHandler>();
        buttonHandler.setHoldBuffer(holdBuffer); //Update hold time in handling to adjust range allowed

        //Adjust button position
        RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();
        buttonRect.anchoredPosition = new Vector2(0f, -15f);

        //Make threshold text
        textDurationObject = Instantiate(textPrefab, puzzleCanvas.transform);
        float holdThreshold = buttonHandler.getHoldThreshold(); //Retrieve holdDuration 'rolled' from ButtonHandler
        textDurationObject.GetComponent<TextMeshProUGUI>().text = holdThreshold.ToString();

        //Adjust text position
        RectTransform textDureationRect = textDurationObject.GetComponent<RectTransform>();
        textDureationRect.anchoredPosition = new Vector2(0f, 25f);

        textCurrentObject = Instantiate(textPrefab, puzzleCanvas.transform);

        //Adjust text position
        RectTransform textCurrentRect = textCurrentObject.GetComponent<RectTransform>();
        textCurrentRect.anchoredPosition = new Vector2(0f, 7f);

        //Subscribe to button event
        buttonHandler.OnHoldComplete += OnHoldComplete;

        Activate();
    }

    private void Update()
    {
        float currentDuration = buttonHandler.getCurrentHoldTime(); //Fetch current hold time from buttonHandler
        float currentDurationRounded = (float)Math.Round(currentDuration, 2);
        textCurrentObject.GetComponent<TextMeshProUGUI>().text = currentDurationRounded.ToString();
    }

    public override void Activate()
    {
        base.Activate();
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
