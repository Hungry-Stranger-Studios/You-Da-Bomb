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

    private void Awake()
    {
        //Puzzle Base
        puzzleName = "Slider Puzzle";
        puzzleGridHeight = 1;
        puzzleGridWidth = 1;
        puzzleType = "Main"; //Label Constant for separate "Constant" grid spawning

        Activate();
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void Solved()
    {
        base.Solved();
        StressManagement.Instance.AdjustStress(-5.0f);
        Destroy(GameObject.FindWithTag("SliderPuzzle"));
    }

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