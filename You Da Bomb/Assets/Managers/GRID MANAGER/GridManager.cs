using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public Vector2Int Position { get; private set; }
    public PuzzleBase Puzzle { get; set; }

    public GridCell(int x, int y)
    {
        Position = new Vector2Int(x, y);
        Puzzle = null;
    }
}

public class GridManager : MonoBehaviour
{
    public static GridManager Instance { get; private set; }

    private GridCell[,] grid;
    private GridCell[,] gridConstant;
    private PuzzleFactory puzzleFactory;

    [Header("Grid Sizing")]
    [SerializeField] private int gridRows = 2;
    [SerializeField] private int gridColumns = 2;
    [SerializeField] private int gridRowsConstant = 2;
    [SerializeField] private int gridColumnsConstant = 1;
    [SerializeField] private float cellSpacing = 1f;
    private float cellSize = 1f;

    [Header("Grid Spawning")]
    [SerializeField] private float puzzleSpawnRate = 5.0f; //How often puzzles spawn in seconds

    private int puzzleCount = 0;

    //new line test
    private HashSet<string> constantpuzzleplaced = new HashSet<string>();

    public int getPuzzleCount() { return puzzleCount; }

    private void Awake() //Initialize the grid
    {
        if (Instance != null && Instance != this) //Only one grid manager
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        grid = new GridCell[gridColumns, gridRows];
        gridConstant = new GridCell[gridColumnsConstant, gridRowsConstant];

        for (int x = 0; x < gridColumns; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {
                grid[x, y] = new GridCell(x, y);
            }
        }

        for (int x = 0; x < gridColumnsConstant; x++)
        {
            for (int y = 0; y < gridRowsConstant; y++)
            {
                gridConstant[x, y] = new GridCell(x, y);
            }
        }
    }

    private void Start()
    {
        puzzleFactory = FindObjectOfType<PuzzleFactory>();
        if (puzzleFactory == null)
        {
            Debug.LogError("PuzzleFactory not found in scene");
            return;
        }

        StartCoroutine(SpawnPuzzles()); //Spawn puzzles in specified interval
    }

    private IEnumerator SpawnPuzzles()
    {
        while (true)
        {
            //Spawn puzzle, place on grid
            PlacePuzzleInGrid();

            yield return new WaitForSeconds(puzzleSpawnRate);
        }
    }

    public void PlacePuzzleInGrid() //Called when adding new puzzle to grid
    {
        GameObject puzzleObject = puzzleFactory.FetchRandomPuzzle();
        PuzzleBase puzzle = puzzleObject.GetComponent<PuzzleBase>();

        if (puzzle == null)
        {
            return;
        }

        if (puzzle.puzzleType == "Constant")
        {
            //new line area start
            if(constantpuzzleplaced.Contains(puzzle.name))
            {
                Destroy(puzzleObject);
                return;
            }
            //new line area end

            //Place in the constant grid
            Vector2Int? placementPosition = FindPlacementForConstantPuzzle(puzzle);

            if (placementPosition.HasValue)
            {
                PlacePuzzleInConstantGrid(puzzle, placementPosition.Value);
                //new line
                constantpuzzleplaced.Add(puzzle.name);
                //new line
            }
            else
            {
                //No spots available for constant
                Destroy(puzzleObject);
            }
        }
        else
        {
            //Place in the main grid
            Vector2Int? placementPosition = FindPlacementForPuzzle(puzzle);

            if (placementPosition.HasValue)
            {
                PlacePuzzle(puzzle, placementPosition.Value);
            }
            else
            {
                //No spots available for normal
                Destroy(puzzleObject);
            }
        }
    }

    private Vector2Int? FindPlacementForPuzzle(PuzzleBase puzzle) //Goes through grid cells, checks for valid
    {
        List<Vector2Int> possiblePositions = new List<Vector2Int>();

        for (int x = 0; x <= gridColumns - puzzle.puzzleGridWidth; x++) //Width
        {
            for (int y = 0; y <= gridRows - puzzle.puzzleGridHeight; y++) //Height
            {
                possiblePositions.Add(new Vector2Int(x, y));
            }
        }

        ShuffleList(possiblePositions); //Randomize spots

        foreach (var position in possiblePositions)
        {
            if (CanPlacePuzzle(puzzle, position))
            {
                return position; //Return valid position
            }
        }

        return null; //No spot found
    }

    private void ShuffleList<T>(List<T> list) //Shuffles list of positions for random placing
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private bool CanPlacePuzzle(PuzzleBase puzzle, Vector2Int position) //Returns bool for spot availability
    {
        //Check if puzzle fits in bounds and doesn't overlap
        for (int x = 0; x < puzzle.puzzleGridWidth; x++)
        {
            for (int y = 0; y < puzzle.puzzleGridHeight; y++)
            {
                int gridX = position.x + x;
                int gridY = position.y + y;

                if (grid[gridX, gridY] != null && grid[gridX, gridY].Puzzle != null) //Cell is occupied
                {
                    return false;
                }
            }
        }

        return true; //Can place puzzle
    }

    private void PlacePuzzle(PuzzleBase puzzle, Vector2Int position) //Assigns puzzle to found open cell
    {
        //Mark cells as occupied
        for (int x = 0; x < puzzle.puzzleGridWidth; x++)
        {
            for (int y = 0; y < puzzle.puzzleGridHeight; y++)
            {
                grid[position.x + x, position.y + y].Puzzle = puzzle;
                puzzleCount++;
            }
        }

        //Position puzzle in world space
        Vector3 worldPosition = CalculateWorldPosition(position);
        puzzle.transform.position = worldPosition;
    }

    private Vector3 CalculateWorldPosition(Vector2Int gridPosition) //Get coordinate based on cell assigned
    {
        //Convert grid coordinates to world coordinates
        float xPosition = gridPosition.x * (cellSize + cellSpacing);
        float yPosition = gridPosition.y * (cellSize + cellSpacing);
        return new Vector3(xPosition, yPosition, 0);
    }

    private Vector2Int? FindPlacementForConstantPuzzle(PuzzleBase puzzle)
    {
        for (int x = 0; x < gridColumnsConstant - puzzle.puzzleGridWidth + 1; x++)
        {
            for (int y = 0; y < gridRowsConstant - puzzle.puzzleGridHeight + 1; y++)
            {
                if (CanPlaceInConstantGrid(puzzle, new Vector2Int(x, y)))
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return null;
    }

    private bool CanPlaceInConstantGrid(PuzzleBase puzzle, Vector2Int position)
    {
        for (int x = 0; x < puzzle.puzzleGridWidth; x++)
        {
            for (int y = 0; y < puzzle.puzzleGridHeight; y++)
            {
                int gridX = position.x + x;
                int gridY = position.y + y;

                Debug.Log($"{gridX} and {gridY}");

                if (gridConstant[gridX, gridY] != null && gridConstant[gridX, gridY].Puzzle != null)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void PlacePuzzleInConstantGrid(PuzzleBase puzzle, Vector2Int position)
    {
        for (int x = 0; x < puzzle.puzzleGridWidth; x++)
        {
            for (int y = 0; y < puzzle.puzzleGridHeight; y++)
            {
                gridConstant[position.x + x, position.y + y].Puzzle = puzzle;
            }
        }

        Vector3 worldPosition = CalculateWorldPositionForConstantGrid(position);
        puzzle.transform.position = worldPosition;
    }

    private Vector3 CalculateWorldPositionForConstantGrid(Vector2Int gridPosition)
    {
        float xPosition = gridPosition.x * (cellSize + cellSpacing);
        float yPosition = -(gridRowsConstant * (cellSize + cellSpacing)) + gridPosition.y * (cellSize + cellSpacing); //Offset below main grid
        return new Vector3(xPosition, yPosition, 0);
    }

    private void OnDrawGizmos()
    {
        if (grid == null) return;

        //Main grid
        for (int x = 0; x < gridColumns; x++)
        {
            for (int y = 0; y < gridRows; y++)
            {
                GridCell cell = grid[x, y];
                Gizmos.color = (cell.Puzzle != null) ? Color.green : Color.red;

                Vector3 cellPosition = new Vector3(x * (cellSize + cellSpacing), y * (cellSize + cellSpacing), 0);
                Gizmos.DrawWireCube(cellPosition, Vector3.one);
            }
        }

        //Constant grid
        for (int x = 0; x < gridColumnsConstant; x++)
        {
            for (int y = 0; y < gridRowsConstant; y++)
            {
                GridCell cell = gridConstant[x, y];
                Gizmos.color = (cell.Puzzle != null) ? Color.blue : Color.yellow;

                Vector3 cellPosition = new Vector3(x * (cellSize + cellSpacing), -(gridRowsConstant * (cellSize + cellSpacing)) + y * (cellSize + cellSpacing), 0);
                Gizmos.DrawWireCube(cellPosition, Vector3.one);
            }
        }
    }
}
