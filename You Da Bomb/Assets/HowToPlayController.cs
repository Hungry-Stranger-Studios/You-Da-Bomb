using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayController : MonoBehaviour
{
    public void MainMenu()
    {
        SceneManager.LoadScene("Start Scene");
    }
}
