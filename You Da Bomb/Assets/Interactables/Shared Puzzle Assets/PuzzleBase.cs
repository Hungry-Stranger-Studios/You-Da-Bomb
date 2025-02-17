using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PuzzleBase : MonoBehaviour
{
    public string puzzleName { get; protected set; }
    public string puzzleType { get; protected set; }
    public int puzzleGridHeight { get; protected set; }
    public int puzzleGridWidth { get; protected set; }
    public Vector2Int puzzleLocation { get; set;  }
    public virtual void Activate() { /*ANIMATION FOR STARTING*/}
    public virtual void Solved() { /*ANIMATION FOR ENDING*/}
}
