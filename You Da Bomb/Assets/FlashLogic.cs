using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLogic : MonoBehaviour
{
    [SerializeField] private GameObject lightbulb1;
    [SerializeField] private GameObject lightbulb2;
    [SerializeField] private GameObject lightbulb3;
    public int TBF; //Time Between Flashes
    public bool isFlashing;
    // Start is called before the first frame update
    void Start()
    {
        lightbulb1.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer1);
        lightbulb2.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer2);
        lightbulb3.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer3);
        StartCoroutine(Flash1());
        StartCoroutine(Flash2());
        StartCoroutine(Flash3());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Flash1()
    {
        while (true)
        {
            lightbulb1.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer1);
            TBF = Random.Range(1, 5);
            yield return new WaitForSeconds(TBF);
            lightrenderer1.color = Color.red;
            yield return new WaitForSeconds(5);
            lightrenderer1.color = Color.green;
        }
    }

    private IEnumerator Flash2()
    {
        while (true)
        {
            lightbulb2.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer2);
            TBF = Random.Range(1, 5);
            yield return new WaitForSeconds(TBF);
            lightrenderer2.color = Color.red;
            yield return new WaitForSeconds(5);
            lightrenderer2.color = Color.green;
        }
    }

    private IEnumerator Flash3()
    {
        while (true)
        {
            lightbulb3.TryGetComponent<SpriteRenderer>(out SpriteRenderer lightrenderer3);
            TBF = Random.Range(1, 5);
            yield return new WaitForSeconds(TBF);
            lightrenderer3.color = Color.red;
            yield return new WaitForSeconds(5);
            lightrenderer3.color = Color.green;
        }
    }
}
