using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public Text pointsText;

    public void Setup(int time, int score)
    {
        gameObject.SetActive(true);
        pointsText.text = "You survived " + time + " seconds and deactivated " + score + " puzzles!";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
