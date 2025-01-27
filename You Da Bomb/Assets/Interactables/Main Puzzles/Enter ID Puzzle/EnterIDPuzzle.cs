using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeTypingPuzzle : PuzzleBase
{
    private TMP_InputField inputField;
    private TextMeshProUGUI codeText;
    private string targetCode;

    [Header("Prefabs")]
    [SerializeField] private GameObject buttonPrefab;

    private void Awake()
    {
        puzzleName = "Code Typing Puzzle";
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

        // Create Code Text
        GameObject codeTextGO = new GameObject("CodeText");
        codeTextGO.transform.SetParent(canvasGO.transform);
        codeText = codeTextGO.AddComponent<TextMeshProUGUI>();
        codeText.fontSize = 24;
        codeText.alignment = TextAlignmentOptions.Center;

        // Configure Code Text Layout
        RectTransform codeTextRect = codeTextGO.GetComponent<RectTransform>();
        codeTextRect.localScale = new Vector3(0.015f, 0.015f, 1f);
        codeTextRect.anchorMin = new Vector2(0.5f, 0.7f);
        codeTextRect.anchorMax = new Vector2(0.5f, 0.7f);
        codeTextRect.pivot = new Vector2(0.5f, 0.5f);
        codeTextRect.anchoredPosition = new Vector2(-1, -19.5f);
        codeTextRect.sizeDelta = new Vector2(200, 50);

        // Create Input Field
        GameObject inputFieldGO = new GameObject("InputField");
        inputFieldGO.transform.SetParent(canvasGO.transform);

        // Add RectTransform
        RectTransform inputFieldRect = inputFieldGO.AddComponent<RectTransform>();
        inputFieldRect.localScale = new Vector3(0.01f, 0.01f, 1f);
        inputFieldRect.anchorMin = new Vector2(0.5f, 0.6f);
        inputFieldRect.anchorMax = new Vector2(0.5f, 0.6f);
        inputFieldRect.pivot = new Vector2(0.5f, 0.5f);
        inputFieldRect.anchoredPosition = new Vector2(-1, -10);
        inputFieldRect.sizeDelta = new Vector2(200, 30);

        // Add InputField Component
        inputField = inputFieldGO.AddComponent<TMP_InputField>();

        // Add Background to Input Field
        Image inputBackground = inputFieldGO.AddComponent<Image>();
        inputBackground.color = Color.white;

        // Configure Input Field Text
        TextMeshProUGUI inputText = new GameObject("InputText").AddComponent<TextMeshProUGUI>();
        inputText.transform.SetParent(inputFieldGO.transform);
        inputField.textComponent = inputText;
        inputText.fontSize = 24;
        inputText.alignment = TextAlignmentOptions.Center;
        inputText.color = Color.black;

        // Set RectTransform for Input Text
        RectTransform inputTextRect = inputText.GetComponent<RectTransform>();
        inputTextRect.localScale = new Vector3(1f, 1f, 1f);
        inputTextRect.anchorMin = new Vector2(0, 0);
        inputTextRect.anchorMax = new Vector2(1, 1);
        inputTextRect.offsetMin = new Vector2(5, 5);
        inputTextRect.offsetMax = new Vector2(-5, -5);
        inputTextRect.anchoredPosition = new Vector2(-1, -0.35f);

        // Add Placeholder Text
        TextMeshProUGUI placeholderText = new GameObject("Placeholder").AddComponent<TextMeshProUGUI>();
        placeholderText.text = "Enter code here...";
        placeholderText.fontSize = 20;
        placeholderText.alignment = TextAlignmentOptions.Center;
        placeholderText.color = Color.gray;
        placeholderText.transform.SetParent(inputFieldGO.transform);
        inputField.placeholder = placeholderText;

        // Set RectTransform for Placeholder Text
        RectTransform placeholderRect = placeholderText.GetComponent<RectTransform>();
        placeholderRect.localScale = new Vector3(1f, 1f, 1f);
        placeholderRect.anchorMin = new Vector2(0, 0);
        placeholderRect.anchorMax = new Vector2(1, 1);
        placeholderRect.offsetMin = new Vector2(5, 5);
        placeholderRect.offsetMax = new Vector2(-5, -5);

        // Create Submit Button
        GameObject submitButtonGO = Instantiate(buttonPrefab, canvasGO.transform);
        Button submitButton = submitButtonGO.GetComponent<Button>();
        TextMeshProUGUI buttonText = submitButtonGO.GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = "Submit";

        // Configure Submit Button Layout
        RectTransform buttonRect = submitButtonGO.GetComponent<RectTransform>();
        buttonRect.localScale = new Vector3(0.015f, 0.015f, 1f);
        buttonRect.anchorMin = new Vector2(0.5f, 0.3f);
        buttonRect.anchorMax = new Vector2(0.5f, 0.3f);
        buttonRect.pivot = new Vector2(0.5f, 0.5f);
        buttonRect.anchoredPosition = new Vector2(-1, 19.5f);
        buttonRect.sizeDelta = new Vector2(100, 30);

        // Add Listener to Submit Button
        submitButton.onClick.AddListener(CheckInput);

        Activate();
    }

    public override void Activate()
    {
        base.Activate();

        // Generate random code
        targetCode = GenerateRandomCode();
        codeText.text = targetCode;
    }

    private string GenerateRandomCode()
    {
        const string chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()";
        int codeLength = Random.Range(3, 8);
        char[] codeArray = new char[codeLength];

        for (int i = 0; i < codeLength; i++)
        {
            codeArray[i] = chars[Random.Range(0, chars.Length)];
        }

        return new string(codeArray);
    }

    private void CheckInput()
    {
        if (inputField.text == targetCode)
        {
            Debug.Log("Correct Code! Puzzle Solved!");
            Solved();
        }
        else
        {
            Debug.Log("Incorrect Code! Try again.");
            inputField.text = ""; // Clear the input field
        }
    }

    public override void Solved()
    {
        base.Solved();
        StressManagement.Instance.AdjustStress(-5.0f);
        Destroy(this.gameObject);
    }
}