using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
    private int width, height;
    private List<Vector2Int> pathCells;
    private List<Vector2Int> route;

    public PathGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public List<Vector2Int> GeneratePath()
    {
        pathCells = new List<Vector2Int>();

        int x = 0;
        int y = Random.Range(1, height - 1);

        while (x < width)
        {
            pathCells.Add(new Vector2Int(x, y));

            bool validMove = false;

            while (!validMove)
            {
                int move = Random.Range(0, 3);

                if (move == 0 || x % 2 == 0 || x > width - 2)
                {
                    x++;
                    validMove = true;
                }
                else if (move == 1 && CellIsEmpty(x, y + 1) && y < height - 2)
                {
                    y++;
                    validMove = true;
                }
                else if (move == 2 && CellIsEmpty(x, y - 1) && y > 2)
                {
                    y--;
                    validMove = true;
                }
            }
        }

        return pathCells;
    }

    public void GenerateCrossroads()
    {
        for (int i = 0; i < pathCells.Count; i++)
        {
            Vector2Int cell = pathCells[i];

            if (cell.x > 2 && cell.x < width - 3 && cell.y > 2 && cell.y < height - 3)
            {
                if (CellIsEmpty(cell.x, cell.y + 3) && CellIsEmpty(cell.x + 1, cell.y + 3) && CellIsEmpty(cell.x + 2, cell.y + 3) &&
                    CellIsEmpty(cell.x - 1, cell.y + 2) && CellIsEmpty(cell.x, cell.y + 2) && CellIsEmpty(cell.x + 1, cell.y + 2) && CellIsEmpty(cell.x + 2, cell.y + 2) && CellIsEmpty(cell.x + 3, cell.y + 2) &&
                    CellIsEmpty(cell.x - 1, cell.y + 1) && CellIsEmpty(cell.x, cell.y + 1) && CellIsEmpty(cell.x + 1, cell.y + 1) && CellIsEmpty(cell.x + 2, cell.y + 1) && CellIsEmpty(cell.x + 3, cell.y + 1) &&
                    CellIsEmpty(cell.x + 1, cell.y) && CellIsEmpty(cell.x + 2, cell.y) && CellIsEmpty(cell.x + 3, cell.y) &&
                    CellIsEmpty(cell.x + 1, cell.y - 1) && CellIsEmpty(cell.x + 2, cell.y - 1))
                {
                    if (CellIsEmpty(cell.x - 1, cell.y + 3) && CellIsEmpty(cell.x, cell.y + 4) && CellIsEmpty(cell.x + 1, cell.y + 4) && CellIsEmpty(cell.x + 2, cell.y + 4) && CellIsEmpty(cell.x + 3, cell.y + 3) && cell.y < height - 4)
                    {
                        if (CellIsEmpty(cell.x - 1, cell.y + 4) && CellIsEmpty(cell.x, cell.y + 5) && CellIsEmpty(cell.x + 1, cell.y + 5) && CellIsEmpty(cell.x + 2, cell.y + 5) && CellIsEmpty(cell.x + 3, cell.y + 4) && cell.y < height - 5)
                        {
                            pathCells.InsertRange(i + 1, new List<Vector2Int> { new(cell.x + 1, cell.y), new(cell.x + 2, cell.y), new(cell.x + 2, cell.y + 1), new(cell.x + 2, cell.y + 2), new(cell.x + 2, cell.y + 3), new(cell.x + 2, cell.y + 4), new(cell.x + 1, cell.y + 4), new(cell.x, cell.y + 4), new(cell.x, cell.y + 3), new(cell.x, cell.y + 2), new(cell.x, cell.y + 1) });
                        }
                        else
                        {
                            pathCells.InsertRange(i + 1, new List<Vector2Int> { new(cell.x + 1, cell.y), new(cell.x + 2, cell.y), new(cell.x + 2, cell.y + 1), new(cell.x + 2, cell.y + 2), new(cell.x + 2, cell.y + 3), new(cell.x + 1, cell.y + 3), new(cell.x, cell.y + 3), new(cell.x, cell.y + 2), new(cell.x, cell.y + 1) });
                        }
                    }
                    else
                    {
                        pathCells.InsertRange(i + 1, new List<Vector2Int> { new(cell.x + 1, cell.y), new(cell.x + 2, cell.y), new(cell.x + 2, cell.y + 1), new(cell.x + 2, cell.y + 2), new(cell.x + 1, cell.y + 2), new(cell.x, cell.y + 2), new(cell.x, cell.y + 1) });
                    }
                }
                if (CellIsEmpty(cell.x, cell.y - 3) && CellIsEmpty(cell.x + 1, cell.y - 3) && CellIsEmpty(cell.x + 2, cell.y - 3) &&
                    CellIsEmpty(cell.x - 1, cell.y - 2) && CellIsEmpty(cell.x, cell.y - 2) && CellIsEmpty(cell.x + 1, cell.y - 2) && CellIsEmpty(cell.x + 2, cell.y - 2) && CellIsEmpty(cell.x + 3, cell.y - 2) &&
                    CellIsEmpty(cell.x - 1, cell.y - 1) && CellIsEmpty(cell.x, cell.y - 1) && CellIsEmpty(cell.x + 1, cell.y - 1) && CellIsEmpty(cell.x + 2, cell.y - 1) && CellIsEmpty(cell.x + 3, cell.y - 1) &&
                    CellIsEmpty(cell.x + 1, cell.y) && CellIsEmpty(cell.x + 2, cell.y) && CellIsEmpty(cell.x + 3, cell.y) &&
                    CellIsEmpty(cell.x + 1, cell.y + 1) && CellIsEmpty(cell.x + 2, cell.y + 1))
                {
                    if (CellIsEmpty(cell.x - 1, cell.y - 3) && CellIsEmpty(cell.x, cell.y - 4) && CellIsEmpty(cell.x + 1, cell.y - 4) && CellIsEmpty(cell.x + 2, cell.y - 4) && CellIsEmpty(cell.x + 3, cell.y - 3) && cell.y > 3)
                    {
                        if (CellIsEmpty(cell.x - 1, cell.y - 4) && CellIsEmpty(cell.x, cell.y - 5) && CellIsEmpty(cell.x + 1, cell.y - 5) && CellIsEmpty(cell.x + 2, cell.y - 5) && CellIsEmpty(cell.x + 3, cell.y - 4) && cell.y > 4)
                        {
                            pathCells.InsertRange(i + 1, new List<Vector2Int> { new(cell.x + 1, cell.y), new(cell.x + 2, cell.y), new(cell.x + 2, cell.y - 1), new(cell.x + 2, cell.y - 2), new(cell.x + 2, cell.y - 3), new(cell.x + 2, cell.y - 4), new(cell.x + 1, cell.y - 4), new(cell.x, cell.y - 4), new(cell.x, cell.y - 3), new(cell.x, cell.y - 2), new(cell.x, cell.y - 1) });
                        }
                        else
                        {
                            pathCells.InsertRange(i + 1, new List<Vector2Int> { new(cell.x + 1, cell.y), new(cell.x + 2, cell.y), new(cell.x + 2, cell.y - 1), new(cell.x + 2, cell.y - 2), new(cell.x + 2, cell.y - 3), new(cell.x + 1, cell.y - 3), new(cell.x, cell.y - 3), new(cell.x, cell.y - 2), new(cell.x, cell.y - 1) });
                        }
                    }
                    else
                    {
                        pathCells.InsertRange(i + 1, new List<Vector2Int> { new(cell.x + 1, cell.y), new(cell.x + 2, cell.y), new(cell.x + 2, cell.y - 1), new(cell.x + 2, cell.y - 2), new(cell.x + 1, cell.y - 2), new(cell.x, cell.y - 2), new(cell.x, cell.y - 1) });
                    }
                }
            }
        }
    }

    public List<Vector2Int> GenerateRoute()
    {
        Vector2Int direction = Vector2Int.right;
        route = new List<Vector2Int>();
        Vector2Int currentCell = pathCells[0];
        
        route.Add(currentCell + Vector2Int.left);

        while (currentCell.x < width - 1)
        {
            route.Add(currentCell);

            if (CellIsTaken(currentCell + direction))
            {
                currentCell = currentCell + direction;
            }
            else if(CellIsTaken(currentCell + Vector2Int.up) && direction != Vector2Int.down)
            {
                direction = Vector2Int.up;
                currentCell = currentCell + direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.down) && direction != Vector2Int.up)
            {
                direction = Vector2Int.down;
                currentCell = currentCell + direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.right) && direction != Vector2Int.left)
            {
                direction = Vector2Int.right;
                currentCell = currentCell + direction;
            }
            else if (CellIsTaken(currentCell + Vector2Int.left) && direction != Vector2Int.right)
            {
                direction = Vector2Int.left;
                currentCell = currentCell + direction;
            }
        }

        route.Add(currentCell + Vector2Int.right);
        route.Add(currentCell + Vector2Int.right);

        return route;
    }

    public bool CellIsEmpty(int x , int y)
    {
        return !pathCells.Contains(new Vector2Int(x, y));
    }

    public bool CellIsTaken(int x , int y)
    {
        return pathCells.Contains(new Vector2Int(x, y));
    }

    public bool CellIsTaken(Vector2Int cell)
    {
        return pathCells.Contains(cell);
    }

    public int GetCellNeighbourValue(int x, int y)
    {
        int returnValue = 0;

        // Down
        if (CellIsTaken(x, y - 1))
        {
            returnValue += 1;
        }

        // Up
        if (CellIsTaken(x, y + 1))
        {
            returnValue += 8;
        }

        // Left
        if (CellIsTaken(x - 1, y))
        {
            returnValue += 2;
        }

        // Right
        if (CellIsTaken(x + 1, y))
        {
            returnValue += 4;
        }

        return returnValue;
    }
}
