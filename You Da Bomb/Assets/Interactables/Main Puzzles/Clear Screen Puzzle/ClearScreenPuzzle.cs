using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ClearScreenPuzzle : PuzzleBase
{
    [Header("Draggable Object Stats")]
    [SerializeField] private int maxObjects = 4;
    [SerializeField] private GameObject draggablePrefab;

    public int objectsDraggedOutside = 0;

    private List<GameObject> spawnedObjects = new List<GameObject>();
    private void Awake()
    {
        //Puzzle Base
        puzzleName = "Clear Screen";
        puzzleGridHeight = 1;
        puzzleGridWidth = 1;
        puzzleType = "Main"; //Label Constant for separate "Constant" grid spawning

        Activate();
    }

    public override void Activate()
    {
        base.Activate();

        int random = Random.Range(1, maxObjects);
        for (int i = 0; i < random; i++)
        {
            InstantiateDraggableObject(new Vector3(i, 0, -1));
        }
    }

    public override void Solved()
    {
        base.Solved();
        foreach (var obj in spawnedObjects)
        {
            Destroy(obj);
        }
        spawnedObjects.Clear();

        GridManager.Instance.OnPuzzleFinished(puzzleLocation, false);
        StressManagement.Instance.AdjustStress(-5.0f);
        Destroy(this.gameObject);
    }

    private void InstantiateDraggableObject(Vector3 localPosition)
    {
        RectTransform parentRect = GetComponent<RectTransform>();
        if (parentRect == null)
        {
            Debug.LogError("Parent does not have a RectTransform!");
            return;
        }

        float padding = 0.03f;

        //Generate random offsets within parent's bounds
        float randomX = Random.Range(-parentRect.rect.width / 2f - padding, parentRect.rect.width / 2f + padding);
        float randomY = Random.Range(-parentRect.rect.height / 2f - padding, parentRect.rect.height / 2f + padding);

        GameObject draggableObject = Instantiate(draggablePrefab, transform); //Make object
        draggableObject.transform.localPosition = new Vector3(randomX, randomY, -1); //Move object to puzzle spot
        spawnedObjects.Add(draggableObject); //Add to list for deletion
        ClearScreenDraggable draggable = draggableObject.GetComponent<ClearScreenDraggable>();
        draggable.Initialize(this); //Add to parent (Puzzle that its assigned to)
    }

    public void NotifyObjectDraggedOutside()
    {
        //Increment count of objects dragged outside
        objectsDraggedOutside++;

        //Check if puzzle is solved
        if (objectsDraggedOutside == spawnedObjects.Count)
        {
            Solved();
        }
    }

    private void OnDestroy()
    {
        //Destroy all spawned draggable objects (Used if no grid cells available for puzzle)
        foreach (GameObject obj in spawnedObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }

        spawnedObjects.Clear();
    }
}
