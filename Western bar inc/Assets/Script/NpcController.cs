using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class NpcController : MonoBehaviour
{
    public Pathfinding pathfinding;
    public TileMapManger TileMap;
    public float Speed;
    public float WayPointRad;

    public TMP_Text TalkingText;
    public float talkingSpeed = 0.08f;

    public List<Vector3> WayPoints;
    public Vector3 NextPosition;
    public Vector2 PreviousPosition;

    // Update is called once per frame

    void Start()
    {
        TileMap = FindObjectOfType<TileMapManger>();
        pathfinding = new Pathfinding(TileMap);
    }

    void Update()
    {
        if (WayPoints != null && WayPoints.Count >= 1)
        {
            Vector2 wayPoint = new Vector2(WayPoints[0].x, WayPoints[0].y + 0.25f);
            Vector3 currentPosition = this.transform.position;
            NextPosition = currentPosition + GetDirection(wayPoint) * Speed * Time.deltaTime;

            if (Vector2.Distance(currentPosition, wayPoint) < WayPointRad && WayPoints.Count > 1)
            {
                WayPoints.RemoveAt(0);
            }

        this.transform.position = NextPosition;
        }
    }

    public void FindNewpath(Vector2 startNode, Vector2 endNode)
    {
        pathfinding = new Pathfinding(TileMap);
        List<PathNode> path = pathfinding.FindPath(((int)startNode.x), (int)startNode.y, (int)endNode.x, (int)endNode.y);
        if (path != null)
        {
            List<Vector3> wayPoints = new List<Vector3>();
            foreach (PathNode node in path)
            {
                wayPoints.Add(node.RealPos);
            }
            this.PreviousPosition = endNode;
            this.WayPoints = wayPoints;
        }
    }

    Vector3 GetDirection(Vector2 pos)
    {
        Vector2 direction = pos - (new Vector2(this.transform.position.x, this.transform.position.y));
        Debug.DrawRay(this.transform.position, direction, Color.green);
        return direction.normalized;
    }

    public IEnumerator SaySomething(string word)
    {
        TalkingText.text = "";
        string Said = "";

        for (int i = 0; i < word.Length; i++)
        {
            Said += word[i];
            TalkingText.text = Said;
            yield return new WaitForSeconds(talkingSpeed);
        }

    }
}
