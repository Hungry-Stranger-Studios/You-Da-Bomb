using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyPuzzle : PuzzleBase
{
    [Header("Radio Stats")]
    [SerializeField] private Transform dial;
    [SerializeField] private float minFrequency = 100f;
    [SerializeField] private float maxFrequency = 2000f;
    [SerializeField] private float targetFrequency = 440f;
    [SerializeField] private float tolerance = 10f;
    [SerializeField] private AudioSource staticAudio;
    [SerializeField] private AudioSource songAudio;
    [SerializeField] private float requiredHoldTime = 1f;

    [Header("Light Sprites")]
    [SerializeField] private GameObject statusLight;
    [SerializeField] private Sprite lightOn;
    [SerializeField] private Sprite lightOff;

    private float currentFrequency;
    private float timer;
    private bool isDialActivated = false;
    private Collider2D dialCollider;

    private void Awake()
    {
        puzzleName = "Frequency Match";
        puzzleGridHeight = 1;
        puzzleGridWidth = 1;
        puzzleType = "Main";

        targetFrequency = Random.Range(minFrequency, maxFrequency);

        //No audio when it first spawns
        staticAudio.loop = true;
        staticAudio.Pause();
        songAudio.loop = true;
        songAudio.volume = 0f;
        songAudio.Pause();

        dialCollider = dial.GetComponent<Collider2D>();
    }

    void Update()
    {
        //Get the dial's rotation
        float rotation = dial.localEulerAngles.z; 
        currentFrequency = Mathf.Lerp(minFrequency, maxFrequency, rotation / 360f);

        if (!isDialActivated && Input.GetMouseButton(0)) //If interacted with or not
        {
            if (IsMouseOverDial()) //If cursor over dial
            {
                isDialActivated = true;
                staticAudio.UnPause();
                songAudio.UnPause();
            }
        }

        if (isDialActivated)
        {
            UpdateAudio();
        }

        //Update audio based on current frequency
        UpdateAudio();

        //Check if frequency matches
        if (Mathf.Abs(currentFrequency - targetFrequency) <= tolerance)
        {
            timer += Time.deltaTime;
            statusLight.GetComponent<SpriteRenderer>().sprite = lightOn;

            //Correct frequency for long enough
            if (timer >= requiredHoldTime)
            {
                Debug.Log("Frequency Matched!");
                Solved();
            }
        }
        else
        {
            //Not correct frequency
            timer = 0f;
            statusLight.GetComponent<SpriteRenderer>().sprite = lightOff;
        }
    }

    private void UpdateAudio()
    {
        // Calculate the distance to the target frequency
        float distance = Mathf.Abs(currentFrequency - targetFrequency);

        // Fade static volume based on distance
        if (distance <= tolerance - 5)
        {
            //Fully focus on the song when correct
            staticAudio.volume = 0f;
            songAudio.volume = 1f;
        }
        else if (distance <= 50f)
        {
            //Balance song and static based on distance
            float normalized = Mathf.InverseLerp(50f, 10f, distance);
            staticAudio.volume = Mathf.Lerp(0.5f, 0f, normalized);
            songAudio.volume = Mathf.Lerp(0f, 1f, normalized);
        }
        else
        {
            // Mostly static beyond 50 units
            staticAudio.volume = 1f;
            songAudio.volume = 0f;
        }
    }

    private bool IsMouseOverDial()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return dialCollider.OverlapPoint(mousePosition);
    }

    public override void Activate()
    {
        base.Activate();
    }

    public override void Solved()
    {
        base.Solved();
        GridManager.Instance.OnPuzzleFinished(puzzleLocation, false);
        StressManagement.Instance.AdjustStress(-5.0f);
        staticAudio.Stop();
        songAudio.Stop();
        Destroy(this.gameObject);
    }
}
