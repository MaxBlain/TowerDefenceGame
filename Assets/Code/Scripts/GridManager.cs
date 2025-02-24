using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] public static int gridWidth = 20;
    [SerializeField] public static int gridHeight = 12;

    private int minPathLength;
    private int maxPathLength;
    
    public GridCellObject[] pathCellObjects;
    public GridCellObject[] sceneryCellObjects;

    public static List<Vector2Int> route;

    private PathGenerator pathGenerator;
    private WaveManager waveManager;

    private void Start()
    {
        GenerateMap(gridWidth, gridHeight);
    }

    public void GenerateMap(int gridWidth, int gridHeight)
    {
        pathGenerator = new PathGenerator(gridWidth, gridHeight);
        waveManager = GetComponent<WaveManager>();

        List<Vector2Int> pathCells = pathGenerator.GeneratePath();

        minPathLength = Mathf.RoundToInt(gridWidth * gridHeight / 5.5f);
        maxPathLength = Mathf.RoundToInt(minPathLength * 1.6f);

        int pathSizeMin = pathCells.Count;
        int pathSizeMax = pathCells.Count;
        while (pathSizeMin <= minPathLength || pathSizeMax >= maxPathLength)
        {
            pathCells = pathGenerator.GeneratePath();
            pathSizeMin = pathCells.Count;
            pathGenerator.GenerateCrossroads();
            pathSizeMax = pathCells.Count;
        }

        StartCoroutine(CreateGrid(pathCells));
    }

    private IEnumerator CreateGrid(List<Vector2Int> pathCells)
    {
        yield return LayPathCells(pathCells);
        yield return LaySceneryCells();
        route = pathGenerator.GenerateRoute();
        waveManager.SetPathRoute(route);
    }

    private IEnumerator LayPathCells(List<Vector2Int> pathCells)
    {
        foreach (Vector2Int pathCell in pathCells)
        {
            int neighbourValue = pathGenerator.GetCellNeighbourValue(pathCell.x, pathCell.y);
            GameObject pathTile = pathCellObjects[neighbourValue].cellPrefab;
            GameObject pathTileCell = Instantiate(pathTile, new Vector2(pathCell.x, pathCell.y), Quaternion.identity);
            pathTileCell.transform.Rotate(0f, 0f, pathCellObjects[neighbourValue].yRotation, Space.Self);
            yield return new WaitForSeconds(0.02f);
        }
    }

    private IEnumerator LaySceneryCells()
    {
        for (int y = gridHeight - 1; y >= 0; y--)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                if (pathGenerator.CellIsEmpty(x, y))
                {
                    int randomSceneryCellIndex = Random.Range(0, sceneryCellObjects.Length);
                    Instantiate(sceneryCellObjects[randomSceneryCellIndex].cellPrefab, new Vector2(x, y), Quaternion.identity);
                    yield return new WaitForSeconds(0.005f);
                }
            }
        }

        yield return null;
    }
}
