using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDTest : PuzzleBase
{
    private void Awake()
    {
        //Puzzle Base
        puzzleName = "DragDrop";
        puzzleGridHeight = 1;
        puzzleGridWidth = 1;
        puzzleType = "Main"; //Label Constant for separate "Constant" grid spawning

        Activate();
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
        Debug.Log("Solved() has been called");
        Destroy(this.gameObject);
    }
}
