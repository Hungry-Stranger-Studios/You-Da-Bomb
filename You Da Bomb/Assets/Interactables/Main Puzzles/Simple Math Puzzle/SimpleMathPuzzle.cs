using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleMathPuzzle : PuzzleBase
{
    private TextMeshProUGUI equationText;
    private int correctAnswer;

    [Header("Prefabs")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject gridPrefab;

    private Transform gridTransform;

    private void Awake()
    {
        puzzleName = "Simple Math Puzzle";
        puzzleGridHeight = 1;
        puzzleGridWidth = 1;
        puzzleType = "Main";

        // Create Canvas
        GameObject canvasGO = new GameObject("Canvas");
        Canvas canvas = canvasGO.AddComponent<Canvas>();
        canvasGO.transform.SetParent(this.transform);
        canvas.renderMode = RenderMode.WorldSpace;

        canvasGO.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasGO.AddComponent<GraphicRaycaster>();

        // Create Grid
        GameObject gridGO = Instantiate(gridPrefab, canvas.transform);
        gridTransform = gridGO.transform;

        // Configure Grid Layout
        GridLayoutGroup gridLayout = gridGO.AddComponent<GridLayoutGroup>();
        gridLayout.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayout.constraintCount = 3;
        gridLayout.cellSize = new Vector2(35, 35);
        gridLayout.spacing = new Vector2(2, 2);

        RectTransform gridRectTransform = gridGO.GetComponent<RectTransform>();
        gridRectTransform.localScale = new Vector3(0.015f, 0.015f, 1f);
        gridRectTransform.anchoredPosition = new Vector2(-1.4f, 0.15f);
        gridRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        gridRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        gridRectTransform.pivot = new Vector2(0.5f, 0.5f);
        gridRectTransform.sizeDelta = new Vector2(60, 20);

        // Create Equation Text
        GameObject equationTextGO = new GameObject("EquationText");
        equationTextGO.transform.SetParent(canvasGO.transform);
        equationText = equationTextGO.AddComponent<TextMeshProUGUI>();
        equationText.text = "Equation";
        equationText.fontSize = 20;
        equationText.alignment = TextAlignmentOptions.Center;

        // Configure Equation Text Layout
        RectTransform equationTextRect = equationTextGO.GetComponent<RectTransform>();
        equationTextRect.localScale = new Vector3(0.015f, 0.015f, 1f);
        equationTextRect.anchorMin = new Vector2(0.5f, 0.7f);
        equationTextRect.anchorMax = new Vector2(0.5f, 0.7f);
        equationTextRect.pivot = new Vector2(0.5f, 0.5f);
        equationTextRect.anchoredPosition = new Vector2(-1.05f, -19.5f);
        equationTextRect.sizeDelta = new Vector2(150, 25);

        Activate();
    }

    public override void Activate()
    {
        base.Activate();

        // Create Grid
        CreateGrid();
        GeneratePuzzle();
    }

    private void CreateGrid()
    {
        // Cleanup previous grid
        foreach (Transform child in gridTransform)
        {
            Destroy(child.gameObject);
        }

        // Assign buttons
        for (int i = 0; i < 6; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, gridTransform);

            Button buttonComponent = newButton.GetComponent<Button>();
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

            if (buttonText != null)
            {
                buttonText.fontSize = 12;
                buttonText.text = "";
            }

            buttonComponent.onClick.AddListener(() => CheckValue(buttonText.text, buttonComponent));
        }
    }

    private void GeneratePuzzle()
    {
        int num0 = Random.Range(1, 3);
        if (num0 == 0) {
            // Generate a simple math question
            int num1 = Random.Range(1, 21);
            int num2 = Random.Range(1, 21);
            correctAnswer = num1 + num2;
            equationText.text = $"{num1} + {num2} = ?";

            // Generate answer options
            List<int> options = GenerateRandomAnswers(correctAnswer);

            // Assign options to buttons created in CreateGrid
            int i = 0;
            foreach (Transform child in gridTransform)
            {
                Button buttonComponent = child.GetComponent<Button>();
                TextMeshProUGUI buttonText = child.GetComponentInChildren<TextMeshProUGUI>();

                int answer = options[i++];
                if (buttonText != null)
                {
                    buttonText.text = answer.ToString();
                }
            }
        }
        else
        {
            // Generate a simple math question
            int num1 = Random.Range(1, 21);
            int num2 = Random.Range(1, 21);
            correctAnswer = num1 - num2;
            equationText.text = $"{num1} - {num2} = ?";

            // Generate answer options
            List<int> options = GenerateRandomAnswers(correctAnswer);

            // Assign options to buttons created in CreateGrid
            int i = 0;
            foreach (Transform child in gridTransform)
            {
                Button buttonComponent = child.GetComponent<Button>();
                TextMeshProUGUI buttonText = child.GetComponentInChildren<TextMeshProUGUI>();

                int answer = options[i++];
                if (buttonText != null)
                {
                    buttonText.text = answer.ToString();
                }
            }
        }
    }

    private List<int> GenerateRandomAnswers(int correct)
    {
        HashSet<int> options = new HashSet<int> { correct };
        while (options.Count < 6)
        {
            int randomOption = Random.Range(correct - 10, correct + 10);
            if (randomOption != correct)
            {
                options.Add(randomOption);
            }
        }
        List<int> shuffledOptions = new List<int>(options);
        shuffledOptions.Sort((a, b) => Random.Range(-1, 2));
        return shuffledOptions;
    }

    private void CheckValue(string value, Button button)
    {
        if (int.TryParse(value, out int intValue))
        {
            if (intValue == correctAnswer)
            {
                OnCorrectAnswer();
            }
            else
            {
                OnWrongAnswer();
            }
        }
        else
        {
            Debug.LogError("Invalid value provided to CheckValue.");
        }
    }

    private void OnCorrectAnswer()
    {
        Debug.Log("Correct Answer! Puzzle Solved!");
        Solved();
    }

    private void OnWrongAnswer()
    {
        Debug.Log("Wrong Answer! Try again.");
        // Do nothing on wrong answer
    }

    public override void Solved()
    {
        base.Solved();
        Destroy(this.gameObject);
    }
}
