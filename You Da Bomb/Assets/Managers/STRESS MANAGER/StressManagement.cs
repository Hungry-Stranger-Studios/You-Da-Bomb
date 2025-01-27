using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressManagement : MonoBehaviour
{
    public static StressManagement Instance { get; private set; }

    [Header("Stress Stats")]
    [SerializeField] private float stressLevel = 1f; //Actual Stress level
    [SerializeField] private float maxStressLevel = 100f;
    [SerializeField] private float timeToIncrease = 2.0f;
    [SerializeField] private float puzzleScaleEffect = 2.0f;

    [Header("Stress Gauge")]
    public Image stressFillImage;
    [SerializeField] private float fillSmoothSpeed = 2.0f; //Speed of the smooth fill animation

    private float targetFillAmount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

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
            GameManager.Instance.EndGame(); //Trigger game over
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
        }
    }
}
