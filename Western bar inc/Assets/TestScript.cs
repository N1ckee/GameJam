using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestScript : MonoBehaviour
{
    public GameObject Text;
    public GameObject Gen;
    public TileMapManger TileMap;

    public Pathfinding pathfinding;

    public NpcController Npc;

    public Vector2 startNode;
    public Vector2 endNode;

    public bool start;
    public bool ShowTiles;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (start) 
        {
            pathfinding = new Pathfinding(TileMap.Width, TileMap.Height, TileMap);
            List<PathNode> path = pathfinding.FindPath(((int)startNode.x), (int)startNode.y, (int)endNode.x, (int)endNode.y);
            if (path != null)
            {
                foreach (PathNode node in path) 
                {
                    Instantiate(Gen, node.RealPos, new Quaternion());
                }
            }

            start = false;
        }

        if (ShowTiles) 
        { 
            foreach (PathNode Node in TileMap.PathNodes)
            {
                SpawnText(Node.Cells, Node.RealPos);
            }

            ShowTiles = false;
        }
    }

    void SpawnText(Vector3Int info, Vector3 realPos)
    {
        var obj = Instantiate(Text, realPos, new Quaternion());
        TMP_Text text = obj.GetComponentInChildren<TMP_Text>();
        text.text = info.x + "," + info.y;
    }
}
