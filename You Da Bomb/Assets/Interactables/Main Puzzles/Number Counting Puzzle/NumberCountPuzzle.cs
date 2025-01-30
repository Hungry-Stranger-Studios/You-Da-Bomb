using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NumberCountPuzzle : PuzzleBase
{
    private List<int> expected = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private List<int> actual = new List<int>();

    private int rows = 3;
    private int columns = 3;
    private int total = 9;
    private int nextExpectedIndex = 0;

    [Header("Prefabs")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject gridPrefab;

    private Transform gridTransform;

    private void Awake()
    {
        puzzleName = "Number Count";
        puzzleGridHeight = 1;
        puzzleGridWidth = 1;
        puzzleType = "Main";

        //Make Canvas
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvasGO.transform.SetParent(this.transform);
        canvas.renderMode = RenderMode.WorldSpace;

        canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.AddComponent<GraphicRaycaster>();

        //Set sorting layer and order
        canvas.overrideSorting = true;
        canvas.sortingLayerName = "PuzzleElement";

        //Make Grid
        GameObject gridGO = Instantiate(gridPrefab, canvas.transform);
        gridTransform = gridGO.transform;

        //Grid sizing
        RectTransform gridRectTransform = gridGO.GetComponent<RectTransform>();
        gridRectTransform.localScale = new Vector3(0.025f, 0.025f, 1f); //Make smaller for puzzle size
        gridRectTransform.anchoredPosition = new Vector2(transform.position.x, transform.position.y); //Adjust to new position
        gridRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        gridRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        gridRectTransform.pivot = new Vector2(0.5f, 0.5f);
        gridRectTransform.sizeDelta = new Vector2(60, 100);

        Activate();
    }

    public override void Activate()
    {
        base.Activate();

        //Make grid
        CreateGrid();
    }

    private void CreateGrid()
    {
        List<int> shuffled = Shuffle(expected);
        int k = 0;

        //Cleanup previous grid
        foreach (Transform child in gridTransform)
        {
            Destroy(child.gameObject);
        }

        //Assign random values for each button
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newButton = Instantiate(buttonPrefab, gridTransform);

                UnityEngine.UI.Button buttonComponent = newButton.GetComponent<UnityEngine.UI.Button>();
                TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText != null)
                {
                    buttonText.text = shuffled[k].ToString();
                    k++;
                }

                int value = shuffled[k - 1]; 
                buttonComponent.onClick.AddListener(() => CheckValue(value, buttonComponent));
            }
        }
    }

    private List<int> Shuffle(List<int> valueList)
    {
        //Make copy of list, randomize it, then return random list
        List<int> shuffled = new List<int>(valueList);
        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(1, i);
            (shuffled[i], shuffled[randomIndex]) = (shuffled[randomIndex], shuffled[i]);
        }
        return shuffled;
    }

    public void CheckValue(int value, UnityEngine.UI.Button pressedButton)
    {
        //Check if sequence matches
        if (value == expected[nextExpectedIndex])
        {
            //If correct
            pressedButton.GetComponent<Image>().color = Color.green;

            //Move to next expected value
            nextExpectedIndex++;

            //Decrement total
            total--;

            //Check if all values are pressed correctly
            if (total == 0)
            {
                Solved();
            }
        }
        else
        {
            //Reset the button colors for all buttons
            nextExpectedIndex = 0;
            total = 9;
            ResetButtonColors();
        }
    }

    private void ResetButtonColors()
    {
        foreach (Transform child in gridTransform)
        {
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                button.GetComponent<Image>().color = new Color32(244, 193, 18, 255);
            }
        }
    }

    public override void Solved()
    {
        base.Solved();
        StressManagement.Instance.AdjustStress(-5.0f);
        Destroy(this.gameObject);
    }
}
