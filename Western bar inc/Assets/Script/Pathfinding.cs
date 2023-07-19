using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding {

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public TileMapManger TileMap;

    private List<PathNode> OpenList;
    private List<PathNode> CloseList;


    public Pathfinding(TileMapManger tileMap)
    {
        TileMap = tileMap;
    }

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY) 
    {
        PathNode startNode = TileMap.GetCell(startX,startY);
        PathNode endNode = TileMap.GetCell(endX,endY);


        OpenList = new List<PathNode> { startNode };
        CloseList = new List<PathNode>();

        for (int x = TileMap.XMin; x < TileMap.XMax; x++) 
        { 
            for (int y = TileMap.YMin; y < TileMap.YMax; y++) 
            { 
                if (!TileMap.CheckCell(x,y))
                {
                    PathNode pathNode = TileMap.GetCell(x, y);
                    pathNode.gCost = int.MaxValue;
                    pathNode.CalulateFCost();
                    pathNode.CameFromNode = null;
                }
            }
        }

        startNode.gCost = 0;
        startNode.hCost = CalculateDistance(startNode, endNode);
        startNode.CalulateFCost();


        while (OpenList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(OpenList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            OpenList.Remove(currentNode);
            CloseList.Add(currentNode);

            List<PathNode> Neighbours = GetNeighbourList(currentNode);

            foreach (PathNode neighbourNode in Neighbours) 
            { 
                if (CloseList.Contains(neighbourNode)) { continue; }

                int tentativeGCost = currentNode.gCost + CalculateDistance(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.CameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistance(neighbourNode, endNode);
                    neighbourNode.CalulateFCost();

                    if(!OpenList.Contains(neighbourNode))
                    {
                        OpenList.Add(neighbourNode);
                    }
                }
            }
        }
        // out of nodes on the OpenList
        return null;
    }

    private List<PathNode> GetNeighbourList(PathNode currentNode)
    {
        List<PathNode> neighbourlist = new List<PathNode>();

        if (currentNode.x - 1 >= TileMap.XMin) 
        {
            if (!TileMap.CheckCell(currentNode.x - 1, currentNode.y))
            {
                // Left
                neighbourlist.Add(GetNode(currentNode.x - 1, currentNode.y));
            }
            if (!TileMap.CheckCell(currentNode.x - 1, currentNode.y - 1))
            {
                // Left Down
                if (currentNode.y - 1 >= TileMap.YMin) { neighbourlist.Add(GetNode(currentNode.x - 1, currentNode.y - 1)); }
            }
            if (!TileMap.CheckCell(currentNode.x - 1, currentNode.y + 1))
            {
                // Left Up
                if (currentNode.y < TileMap.YMax) { neighbourlist.Add(GetNode(currentNode.x - 1, currentNode.y + 1)); }
            }
        }

        if (currentNode.x < TileMap.XMax) 
        {
            if (!TileMap.CheckCell(currentNode.x + 1, currentNode.y))
            {
                // Right
                neighbourlist.Add(GetNode(currentNode.x + 1, currentNode.y));
            }
            if (!TileMap.CheckCell(currentNode.x + 1, currentNode.y - 1))
            {
                // Right Down
                if (currentNode.y - 1 >= TileMap.YMin) { neighbourlist.Add(GetNode(currentNode.x + 1, currentNode.y - 1)); }
            }
            if (!TileMap.CheckCell(currentNode.x + 1, currentNode.y +1))
            {
                // Right Up
                if (currentNode.y < TileMap.YMax) { neighbourlist.Add(GetNode(currentNode.x + 1, currentNode.y + 1)); }
            }
        }

        if (!TileMap.CheckCell(currentNode.x, currentNode.y - 1))
        {
            // Down
            if (currentNode.y - 1 >= TileMap.YMin) { neighbourlist.Add(GetNode(currentNode.x, currentNode.y - 1)); }
        }
        if (!TileMap.CheckCell(currentNode.x, currentNode.y + 1))
        {
            // Up
            if (currentNode.y < TileMap.YMax) { neighbourlist.Add(GetNode(currentNode.x, currentNode.y + 1)); }
        }
        return neighbourlist;
    }

    private PathNode GetNode(int x, int y) 
    {
        return TileMap.GetCell(x, y);
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.CameFromNode != null) 
        { 
            path.Add(currentNode.CameFromNode);
            currentNode = currentNode.CameFromNode;
        }
        path.Reverse();
        return path;
    }


    private int CalculateDistance(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaning = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaning;

    }

    private PathNode GetLowestFCostNode(List<PathNode> PathNodeList)
    {
        PathNode lowestFCostNode = PathNodeList[0];
        for (int i = 1; i < PathNodeList.Count; i++) 
        { 
            if (PathNodeList[i].fCost < lowestFCostNode.fCost) 
            { 
                lowestFCostNode = PathNodeList[i];
            }
        }
        return lowestFCostNode;
    }
}
