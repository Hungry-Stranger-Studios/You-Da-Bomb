using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleFactory : MonoBehaviour
{
    [Header("Puzzle Prefabs")]
    [SerializeField] private List<GameObject> puzzlePrefabs = new List<GameObject>();

    
    public GameObject FetchRandomPuzzle()
    {
        if (puzzlePrefabs.Count == 0)
        {
            Debug.LogError("No puzzles provided");
            return null;
        }

        int random = Random.Range(0,puzzlePrefabs.Count - 1);
        GameObject selectedPrefab = puzzlePrefabs[random];

        return Instantiate(selectedPrefab);
    }
}
