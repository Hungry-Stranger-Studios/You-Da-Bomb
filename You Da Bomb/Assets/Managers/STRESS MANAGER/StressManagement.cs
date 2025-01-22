using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressManagement : MonoBehaviour
{
    public static StressManagement Instance { get; private set; }

    [Header("Stress Stats")]
    [SerializeField] private float stressLevel = 1f; 
    [SerializeField] private float MaxStressLevel = 100f;
    [SerializeField] private float timeToIncrease = 2.0f;
    [SerializeField] private float puzzleScaleEffect = 2.0f;

    [Header("Stress Gauge")]
    public Image stressFillImage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        stressFillImage.fillAmount = 0; //Start at 0 stress
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
            Debug.Log(GridManager.Instance.getPuzzleCount());
            float puzzleFactor = GridManager.Instance.getPuzzleCount() / 100f * puzzleScaleEffect; 
            float increaseAmount = stressLevel * puzzleFactor;
            AdjustStress(increaseAmount);
        }
    }

    public void AdjustStress(float amount)
    {
        stressLevel += amount;

        if (stressLevel <= 0)
        {
            stressLevel = 1; //Minimum stress level is 1
        }

        stressLevel = Mathf.Clamp(stressLevel, 0, MaxStressLevel);
        UpdateStressMeterUI();

        if (stressLevel >= MaxStressLevel)
        {
            GameManager.Instance.EndGame(); //Trigger game over if stress is maxed out
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
            stressFillImage.fillAmount = stressLevel / MaxStressLevel; 
        }
    }
}
