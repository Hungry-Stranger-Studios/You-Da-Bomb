using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class FlashLogic : PuzzleBase
{
    [SerializeField] private GameObject lightbulb1;
    [SerializeField] private GameObject lightbulb2;
    [SerializeField] private GameObject lightbulb3;
    [SerializeField] private Sprite lightOnSprite;
    [SerializeField] private Sprite lightOffSprite;
    public int TBF; //Time Between Flashes
    public bool isFlashing1;
    public bool button1clicked;
    public bool button2clicked;
    public bool button3clicked;
    public bool isFlashing2;
    public bool isFlashing3;

    public bool L1Fail;
    public bool L2Fail;
    public bool L3Fail;
    // Start is called before the first frame update

    public void Awake()
    {
        L1Fail = false;
        L2Fail = false;
        L3Fail = false;

        puzzleName = "Flashing Light";
        puzzleGridHeight = 1;
        puzzleGridWidth = 1;
        puzzleType = "Constant";

        Activate();
    }

    public override void Activate()
    {
        base.Activate();
    }
    void Start()
    {
        lightbulb1.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer1);
        lightbulb2.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer2);
        lightbulb3.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer3);
        StartCoroutine(Flash1());
        StartCoroutine(Flash2());
        StartCoroutine(Flash3());
        
    }

    private void Update()
    {
        CheckForLoss();
    }



    private IEnumerator Flash1()
    {
        while (true)
        {
            lightbulb1.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer1);
            TBF = Random.Range(5, 10);
            yield return new WaitForSeconds(TBF);

            //start flashing
            isFlashing1 = true;
            lightrenderer1.sprite = lightOnSprite;
            button1clicked = false;
            bool taskCompleted = false;

            float timer = 5f;
            while (timer > 0)
            {
                if (button1clicked)
                {
                    taskCompleted = true;
                    lightrenderer1.sprite = lightOffSprite;
                    break;
                }
                timer -= Time.deltaTime;
                yield return null;
            }

            if(!taskCompleted)
            {
                L1Fail = true;
            }

            isFlashing1 = false;
            lightrenderer1.sprite = lightOffSprite;
        }
    }

    private IEnumerator Flash2()
    {
        while (true)
        {
            lightbulb2.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer2);
            TBF = Random.Range(8, 15);
            yield return new WaitForSeconds(TBF);

            //start flashing
            isFlashing2 = true;
            lightrenderer2.sprite = lightOnSprite;
            button2clicked = false;
            bool taskCompleted = false;

            float timer = 5f;
            while (timer > 0)
            {
                if (button2clicked)
                {
                    taskCompleted = true;
                    lightrenderer2.sprite = lightOffSprite;
                    break;
                }
                timer -= Time.deltaTime;
                yield return null;
            }

            if (!taskCompleted)
            {
                L2Fail = true;
            }

            isFlashing2 = false;
            lightrenderer2.sprite = lightOffSprite;
        }
    }

    private IEnumerator Flash3()
    {
        while (true)
        {
            lightbulb3.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer3);
            TBF = Random.Range(11, 17);
            yield return new WaitForSeconds(TBF);

            //start flashing
            isFlashing3 = true;
            lightrenderer3.sprite = lightOnSprite;
            button3clicked = false;
            bool taskCompleted = false;

            float timer = 5f;
            while (timer > 0)
            {
                if (button3clicked)
                {
                    taskCompleted = true;
                    lightrenderer3.sprite = lightOffSprite;
                    break;
                }
                timer -= Time.deltaTime;
                yield return null;
            }

            if (!taskCompleted)
            {
                L3Fail = true;
            }

            isFlashing3 = false;
            lightrenderer3.sprite = lightOffSprite;
        }
    }

    public void ClickButton1()
    {
        if (isFlashing1)
        {
            button1clicked = true;
        }
    }

    public void ClickButton2()
    {
        if (isFlashing2)
        {
            button2clicked = true;
        }
    }

    public void ClickButton3()
    {
        if (isFlashing3)
        {
            button3clicked = true;
        }
    }

    public void CheckForLoss()
    {
        if(L1Fail && L2Fail && L3Fail)
        {
            StressManagement.Instance.AdjustStress(50.0f);
        }
    }
}
