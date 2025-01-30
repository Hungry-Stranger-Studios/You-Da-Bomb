using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class StressManagement : MonoBehaviour
{
    public static StressManagement Instance { get; private set; }
    public GameOverScript gameOverScript;

    [Header("Stress Stats")]
    [SerializeField] private float stressLevel = 1f; //Actual Stress level
    [SerializeField] private float maxStressLevel = 100f;
    [SerializeField] private float timeToIncrease = 2.0f;
    [SerializeField] private float puzzleScaleEffect = 2.0f;

    [Header("Stress Gauge")]
    public Image stressFillImage;
    [SerializeField] private float fillSmoothSpeed = 2.0f; //Speed of the smooth fill animation
    [SerializeField] private float magnitude = 2.0f;

    private float targetFillAmount;
    private RectTransform imageRect;
    private Vector3 originalPosition;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        originalPosition = this.transform.position;
        Debug.Log(originalPosition);
        stressLevel = Mathf.Clamp(stressLevel, 1, maxStressLevel);
        UpdateStressMeterUI();
    }
    private void Start()
    {
         StartCoroutine(IncreaseStress()); //Increase Stress based on interval
    }

    private IEnumerator IncreaseStress()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToIncrease);
            int puzzleCount = GridManager.Instance.getPuzzleCount();
            float puzzleStressFactor = (puzzleCount / 100f) * puzzleScaleEffect;
            float increaseAmount = stressLevel * puzzleStressFactor;

            AdjustStress(increaseAmount);
        }
    }

    private void Update()
    {
        UpdateStressMeterUI();
    }


    public void AdjustStress(float amount)
    {
        stressLevel += amount;

        //Clamp stress level within valid range
        stressLevel = Mathf.Clamp(stressLevel, 1, maxStressLevel);

        targetFillAmount = stressLevel / maxStressLevel;

        //Update UI
        UpdateStressMeterUI();

        //Check for game over
        if (stressLevel >= maxStressLevel)
        {
            gameOverScript.Run();
        }
    }

    public float GetStressLevel()
    {
        return stressLevel;
    }

    private void UpdateStressMeterUI()
    {
        if (stressFillImage != null)
        {
            float currentFill = stressFillImage.fillAmount;
            stressFillImage.fillAmount = Mathf.Lerp(currentFill, targetFillAmount, fillSmoothSpeed * Time.deltaTime);
            if (currentFill >= 50)
            {
                imageRect = stressFillImage.GetComponent<RectTransform>();
                StartCoroutine(Shake() );
            }
        }
    }

    private System.Collections.IEnumerator Shake()
    {        
        float offsetX = Random.Range(-1f, 1f) * magnitude;
        float offsetY = Random.Range(-1f, 1f) * magnitude;

        imageRect.anchoredPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

        yield return null;

        imageRect.anchoredPosition = originalPosition;
    }
}
