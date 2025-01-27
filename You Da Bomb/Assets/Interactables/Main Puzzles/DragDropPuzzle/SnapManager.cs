using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapManager : MonoBehaviour
{
    public List<Snap> snaps;
    private bool hasWon = false;

    private void Update()
    {
        CheckForWin();
    }

    private void CheckForWin()
    {
        if (hasWon) return;
        foreach (Snap snap in snaps)
        {
            if(!snap.isCorrectlySnapped)
            {
                return;
            }
        }
        hasWon = true;
        Debug.Log("SUCCESS");
    }

}
