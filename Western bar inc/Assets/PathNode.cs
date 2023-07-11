using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private TileMapManger tileMap;
    public Vector3Int Cells;
    public Vector3 RealPos;

    public int x;
    public int y;

    public int gCost;
    public int hCost;
    public int fCost;

    public PathNode CameFromNode;
    public PathNode(Vector3Int cells, Vector3 realPos) 
    {
        x = cells.x;
        y = cells.y;
        Cells = cells;
        RealPos = realPos;
    }

    public void CalulateFCost()
    {
        fCost = gCost + hCost;
    }
}
