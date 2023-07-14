using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Staff : MonoBehaviour
{
    public NpcController Controller;
    public OrderManger orderManger;

    public TileMapManger TileMap;
    public Pathfinding pathfinding;

    // Start is called before the first frame update
    void Start()
    {
        orderManger = FindAnyObjectByType<OrderManger>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FindNewpath(Vector2 startNode, Vector2 endNode)
    {
        pathfinding = new Pathfinding(TileMap.Width, TileMap.Height, TileMap);
        List<PathNode> path = pathfinding.FindPath(((int)startNode.x), (int)startNode.y, (int)endNode.x, (int)endNode.y);
        if (path != null)
        {
            List<Vector3> wayPoints = new List<Vector3>();
            foreach (PathNode node in path)
            {
                wayPoints.Add(node.RealPos);
            }

            wayPoints.Add(path[path.Count - 1].RealPos);
            Controller.WayPoints = wayPoints;
        }
    }
}
