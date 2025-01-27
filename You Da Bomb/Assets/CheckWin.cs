using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWin : MonoBehaviour
{
    public static CheckWin Instance; // Singleton instance

    private int totalSnapPoints = 0;
    private int correctlySnappedCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Count the total number of Snap scripts in the scene
        totalSnapPoints = FindObjectsOfType<Snap>().Length;
    }

    public void UpdateSnapState(bool isCorrectlySnapped)
    {
        if (isCorrectlySnapped)
        {
            correctlySnappedCount++;
        }
        else
        {
            correctlySnappedCount--;
        }

        // Check for win condition
        if (correctlySnappedCount == totalSnapPoints)
        {
            RegisterWin();
        }
    }

    private void RegisterWin()
    {
        Debug.Log("All objects are correctly snapped! You win!");
        // Add further win logic here, e.g., UI updates, level progression, etc.
    }
}
