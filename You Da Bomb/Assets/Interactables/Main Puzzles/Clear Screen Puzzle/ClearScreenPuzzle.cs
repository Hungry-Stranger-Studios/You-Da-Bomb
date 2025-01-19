using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScreenPuzzle : PuzzleBase
{
    private void Awake()
    {
        puzzleName = "Clear Screen";
        puzzleGridHeight = 1;
        puzzleGridWidth = 1;
        puzzleType = "Main"; //Label Constant for separate "Constant" grid spawning
    }

    public override void Activate()
    {
        base.Activate();
        Debug.Log($"{puzzleName} puzzle started!");
    }

    public override void Solved()
    {
        base.Solved();
        Debug.Log($"{puzzleName} puzzle solved!");
    }
}
