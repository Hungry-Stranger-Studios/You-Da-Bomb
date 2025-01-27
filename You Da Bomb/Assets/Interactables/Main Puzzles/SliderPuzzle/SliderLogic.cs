using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderLogic : PuzzleBase
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI slidertext;
    [SerializeField] private TextMeshProUGUI goalText;
    [SerializeField] private GameObject bulb;
    public int Goal;

    private float correctValueStartTime;
    private bool isOnCorrectValue = false;
    public float requiredHoldTime = 1f;

    void Start()
    {
        bulb.TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer);
        Goal = Random.Range(1, 10);
        goalText.text = Goal.ToString();

        slider.onValueChanged.AddListener((v) =>
        {
            slidertext.text = v.ToString("0");

            if ((int)v == Goal)
            {
                if (!isOnCorrectValue)
                {
                    correctValueStartTime = Time.time;
                    isOnCorrectValue = true;
                    bulb.GetComponent<SpriteRenderer>().color = Color.green;
                }
            }
            else
            {
                renderer.color = Color.red;
                isOnCorrectValue = false;
            }
        });
    }

    void Update()
    {
        if (isOnCorrectValue && Time.time - correctValueStartTime >= requiredHoldTime)
        {
            Debug.Log("SUCCESS!");
            Solved();
            isOnCorrectValue = false;
            
            

        }
    }
}