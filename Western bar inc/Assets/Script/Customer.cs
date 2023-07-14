using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    public NpcController Controller;
    public OrderManger orderManger;

    public TileMapManger TileMap;
    public Pathfinding pathfinding;
    public TMP_Text TalkingText;

    public Vector2 Spawn;

    public string[] favoriteDrinks = { "Beer", "Beer", "Beer" };
    private float Drunkness = 0;
    private float funness = 5;
    private float Awakness = 10;
    private int currentTask = 0;
    public float talkingSpeed = 0.08f;

    public bool Started;

    // Start is called before the first frame update
    void Start()
    {
        orderManger = FindAnyObjectByType<OrderManger>();
        NewFavoriteDrink();
        TalkingText = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Started)
        {
            orderManger.WantsToQue(this);

            Started = false;
        }

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

    private void NewFavoriteDrink() 
    {
        for (int i = 0; i < favoriteDrinks.Length; i++) 
        {
            int rand = Random.Range(0, orderManger.DrinkMenu.Count());
            favoriteDrinks[i] = orderManger.DrinkMenu[rand];
        }
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