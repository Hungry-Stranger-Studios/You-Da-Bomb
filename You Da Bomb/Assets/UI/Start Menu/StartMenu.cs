using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RunTutorial()
    {
        SceneManager.LoadScene("HowToPlay Scene");
    }

    public void RunCredits()
    {
        SceneManager.LoadScene("Credits Scene");
    }
}
