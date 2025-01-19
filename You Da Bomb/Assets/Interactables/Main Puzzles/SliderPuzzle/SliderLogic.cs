using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderLogic : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI slidertext;
    [SerializeField] private TextMeshProUGUI goalText;
    [SerializeField] private GameObject bulb;

    public int Goal;
    // Start is called before the first frame update
    void Start()
    {

        bulb.TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer);
        Goal = Random.Range(0, 10);
        goalText.text = Goal.ToString();

 
        slider.onValueChanged.AddListener((v) =>
        {
            slidertext.text = v.ToString("0");


            if ((int)v == Goal)
            {
                StartCoroutine(wait());
                Debug.Log("SUCCESS!");
                renderer.color = Color.green;
            }
            else
            {
                renderer.color = Color.red;
                StopCoroutine(wait());
            }
        });
    }

    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
    }



    
}
