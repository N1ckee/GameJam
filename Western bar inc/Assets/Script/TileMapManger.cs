using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapManger : MonoBehaviour
{
    public List<PathNode> PathNodes;
    public Tilemap tilemap;

    public int Height;
    public int Width;

    public int XMax;
    public int XMin;
    public int YMax;
    public int YMin;

    void Start()
    {
        PathNodes = new List<PathNode>();

        // Get the bounds of the tilemap
        BoundsInt bounds = tilemap.cellBounds;

        Width = bounds.xMax - bounds.xMin;
        Height = bounds.yMax - bounds.yMin;

        XMax = bounds.max.x;
        XMin =bounds.min.x;
        YMax = bounds.max.y;
        YMin = bounds.min.y;

        // Iterate through each cell in the tilemap
        for (int x = bounds.min.x; x < bounds.max.x; x++)
        {
            for (int y = bounds.min.y; y < bounds.max.y; y++)
            {
                // Get the position of the current cell
                Vector3Int cellPos = new Vector3Int(x, y, 0);

                // Check if the cell contains a tile
                if (tilemap.HasTile(cellPos))
                {
                    // Do something with the position, like printing it

                    PathNode node = new PathNode(cellPos, tilemap.CellToWorld(cellPos));
                    PathNodes.Add(node);
                    // Debug.Log("Tile position: " + cellPos);
                }
            }
        }
    }

    public PathNode GetCell(int x, int y) 
    { 
        if (CheckCell(x,y)) { return null; }
        int cellPos = PathNodes.FindIndex(Node => Node.x == x && Node.y == y);
        return PathNodes[cellPos];
    }

    public bool CheckCell(int x, int y)
    {
        if (PathNodes.FindIndex(Node => Node.x == x && Node.y == y) == -1) 
        {
            return true;
        }
        return false;
    }
}
