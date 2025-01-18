using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsPaused { get; private set; }
    private bool isGameActive;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //Persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        isGameActive = false;
        LoadMenuScene();
    }

    private void LoadMenuScene()
    {
        Debug.Log("Menu displayed");
    }

    private void LoadMainScene()
    {
        Debug.Log("Main displayed");
    }
}
