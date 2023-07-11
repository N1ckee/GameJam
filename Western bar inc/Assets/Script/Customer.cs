using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public NpcController Controller;

    public TileMapManger TileMap;
    public Pathfinding pathfinding;

    public Vector2 Spawn;

    public Vector2[] QueStations;
    public Vector2[] PayStations;
    public Vector2[] SitStations;
    public Vector3[] GetStations;

    bool IsWaiting = false;
    bool IsWalking = false;
    private float Drunkness = 0;
    private float funness = 5;
    private float Awakness = 10;

    public bool Started;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Started) 
        {
            OrderDrink();
            Started = false;
        }
        if (Controller.WayPoints.Count <= 1) 
        { 
            IsWalking = false;
        }
    }

    public void FindNewpath(Vector2 startNode, Vector2 endNode) 
    {
        IsWalking = true;
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

    public void OrderDrink() 
    {
        FindNewpath(Spawn, PayStations[0]);
    }
}
