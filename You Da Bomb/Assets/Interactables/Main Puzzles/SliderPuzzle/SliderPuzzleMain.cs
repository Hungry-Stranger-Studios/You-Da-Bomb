using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : PuzzleBase
{
    private void Awake()
    {
        //Puzzle Base
        puzzleName = "Slider Puzzle";
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
        StressManagement.Instance.AdjustStress(-5.0f);
        Destroy(this.gameObject);
    }
}
