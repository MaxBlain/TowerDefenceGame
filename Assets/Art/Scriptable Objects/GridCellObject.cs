using UnityEngine;

[CreateAssetMenu(fileName = "GridCell", menuName = "TowerDefence/Grid Cell")]
public class GridCellObject : ScriptableObject
{
    public enum CellType { Path, Plot }

    public CellType cellType;
    public GameObject cellPrefab;
    public int yRotation;
}