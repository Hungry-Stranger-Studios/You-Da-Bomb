using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScreenPuzzle : PuzzleBase
{
    private void Awake()
    {
        puzzleName = "Clear Screen";
        puzzleGridHeight = 2;
        puzzleGridWidth = 1;
        puzzleType = "Constant";
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
