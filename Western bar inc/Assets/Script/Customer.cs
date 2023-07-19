using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Customer : MonoBehaviour
{
    public NpcController Controller;
    public OrderManger orderManger;
    public IntrestAreas areaAreas;
    public ShopStats shopStats;

    public string[] favoriteDrinks = { "Beer", "Beer", "Beer" };
    private int Thirsty = 30;
    private float ThirstyTimer;
    private int RandomWait = 2;



    public bool IsHoldingDrink = false;

    public bool Orderd = false;
    public string OrderdDrink = "Nothing";

    public bool Ordering;
    public bool LookAround;
    public bool DoSomthing;

    private float ActionTime;

    // Start is called before the first frame update
    void Start()
    {
        orderManger = FindObjectOfType<OrderManger>();
        areaAreas = FindObjectOfType<IntrestAreas>();
        shopStats = FindObjectOfType<ShopStats>();

        NewFavoriteDrink();
        Speed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Ordering && Controller.WayPoints.Count() <= 1)
        {
            bool CanQue = orderManger.CustomerToQue(this);

            if (CanQue)
            {
                Ordering = false;
            }
        }

        if (!Orderd)
        {
            MakeAnAction();
            GetThirsty();
        }

        Debug.Log(Thirsty);

    }

    public void Speed()
    {
        double newSpeed = 0.5 + (0.15 * shopStats.CustomerSpeed);
        Controller.Speed = (float)newSpeed;
    }

    private void GetThirsty()
    {

        if (ThirstyTimer > RandomWait)
        {
            RandomWait = Random.Range(0, 5);
            Thirsty++;

            if (Thirsty >= 70)
            {
                Ordering = true;
                Thirsty = 0;
            }
            ThirstyTimer = 0;
        }

        ThirstyTimer += Time.fixedDeltaTime;
    }

    private void NewFavoriteDrink()
    {
        for (int i = 0; i < favoriteDrinks.Length; i++)
        {
            int rand = Random.Range(0, orderManger.DrinkMenu.Count());
            favoriteDrinks[i] = orderManger.DrinkMenu[rand];
        }
    }

    public void MakeAnAction()
    {
            
        if (Controller.WayPoints.Count() <= 1 && ActionTime >= 3)
        {
            WalkToRandomPoints(areaAreas.SpotList[0].Pos.ToArray());
            ActionTime = 0;
        }

        ActionTime += Time.fixedDeltaTime;
        
    }

    public void DrinkIdea()
    {
        int RandDrink = Random.Range(0, favoriteDrinks.Count());
        OrderdDrink = favoriteDrinks[RandDrink];
    }

    public void WalkToRandomPoints(Vector2Int[] RandomPostions)
    {
        int randPos = Random.RandomRange(0, RandomPostions.Length);

        Controller.FindNewpath(Controller.PreviousPosition, RandomPostions[randPos]);
    }
}