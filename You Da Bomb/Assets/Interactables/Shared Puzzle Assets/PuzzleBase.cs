using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleBase : MonoBehaviour
{
    public string puzzleName { get; protected set; }
    public string puzzleType { get; protected set; }
    public int puzzleGridHeight { get; protected set; }
    public int puzzleGridWidth { get; protected set; }
    public virtual void Activate()
    {
        Debug.Log($"{puzzleName} activated");
    }

    public virtual void Solved()
    {
        Debug.Log($"{puzzleName} solved");
    }
}
