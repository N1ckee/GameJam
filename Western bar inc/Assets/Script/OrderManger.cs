using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class OrderManger : MonoBehaviour
{

    public List<Customer> Orders;
    public List<Staff> StaffOrders;
    public DrinkList DrinkInstructions;
    public ShopStats TheShopStats;

    public bool IsTakeingCustomer = false;

    public string[] DrinkMenu; 
    public float PayTime = 2;
    public float ServTime = 5;

    private float RealPayTime;
    private float RealServTime;
    private bool OneTime = false;
    private bool IsServing = false;

    [Header("CustomerSpots")]
    public List<Vector2Int> QueSpots;
    public List<Customer> Queing;
    bool IsWaitingInQue = false;

    public List<Vector2Int> PayArea;
    public List<Vector2Int> Seats;
    public List<Vector2Int> ServArea;
    public List<Vector2Int> WaitAreas;

    [Header("Staff")]
    public List<Staff> AvailableStaff;
    bool WaitingForStaff = false;
    public List<Vector2Int> StaffPayArea;
    public List<Vector2Int> Shelfs;
    public List<Vector2Int> StaffServArea;

    private DrinkMaking TheMixing;

    // Start is called before the first frame update
    void Start()
    {
        RealPayTime = PayTime;
        RealServTime = ServTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Queing.Count > 0 && AvailableStaff.Count > 0) 
        { 
           TakeOrder();
        }
        if (Orders.Count > 0 && StaffOrders.Count > 0)
        {
            MakeDrink();
        }
    }

    public void TakeOrder() 
    {
        Customer TheCustomer = Queing[0];
        Staff TheStaff = AvailableStaff[0];

        if (TheCustomer.Controller.WayPoints.Count <= 1 && TheStaff.Controller.WayPoints.Count <= 1 && TheStaff.NothingToDo && !TheStaff.IsTakingOrder)
        {
            WalkTo(TheStaff.Controller, StaffPayArea[0]);
            StartCoroutine(TheCustomer.Controller.SaySomething("Howdy"));
            TheStaff.IsTakingOrder = true;
        }

        if (TheStaff.IsTakingOrder) 
        {
            Orders.Add(TheCustomer);
            StaffOrders.Add(TheStaff);

            WalkTo(TheCustomer.Controller, PayArea[0]);
            StartCoroutine(TheStaff.Controller.SaySomething("So what do you want?"));

            Queing.Remove(TheCustomer);
            AvailableStaff.Remove(TheStaff);
        }
    }

    public void MakeDrink()
    {
        Customer TheCustomer = Orders[0];
        Staff TheStaff = StaffOrders[0];
        string TheDrink = "";

        if (!TheCustomer.Orderd)
        { 
            StartCoroutine(TheCustomer.Controller.SaySomething("Hmmm"));
            TheCustomer.DrinkIdea();
            TheDrink = TheCustomer.OrderdDrink;
            StartCoroutine(TheCustomer.Controller.SaySomething("Give me some " + TheDrink.ToLower()));

            TheCustomer.Orderd = true;

            for (int i = 0; i < DrinkInstructions.AllDrinks.Count; i++)
            {
                if (TheDrink == DrinkInstructions.AllDrinks[i].Drink)
                {
                    TheMixing = DrinkInstructions.AllDrinks[i];
                    break;
                }
            }

            TheShopStats.ChangeMoney(TheMixing.Cash);
        }

        if (TheMixing != null)
        {
            StartCoroutine(TheMixing.StartMaking(TheStaff.Controller));

            if (TheMixing.CurrentTask >= TheMixing.makingPositionList.Length)
            {
                ServCustomer(TheCustomer, TheStaff, TheDrink, TheMixing);
            }
        }

    }

    public void ServCustomer(Customer TheCustomer, Staff TheStaff, string TheDrink, DrinkMaking Ins)
    {
        if (!IsServing)
        {
            WalkTo(TheCustomer.Controller, ServArea[0]);
            WalkTo(TheStaff.Controller, StaffServArea[0]);
            IsServing = true;
        }

        if (TheCustomer.Controller.WayPoints.Count <= 1 && TheStaff.Controller.WayPoints.Count <= 1)
        {

            // end 


            StartCoroutine(TheStaff.Controller.SaySomething("Here you go pal!"));
            StartCoroutine(TheCustomer.Controller.SaySomething("Thanks!"));
            TheCustomer.IsHoldingDrink = true;
            TheCustomer.Orderd = false;
            Ins.CurrentTask = 0;
            TheStaff.NothingToDo = true;


            AvailableStaff.Add(TheStaff);

            WaitingForStaff = false;

            Orders.Remove(TheCustomer);
            StaffOrders.Remove(TheStaff);

        }
    }

    public bool CustomerToQue(Customer TheCustomer)
    {
        if (Queing.Count < QueSpots.Count)
        {
            Queing.Add(TheCustomer);
            WalkTo(TheCustomer.Controller, QueSpots[Queing.Count - 1]);
            return true;
        }

        return false;
    } 

    public void WalkTo(NpcController TheController, Vector2 position)
    {
        TheController.FindNewpath(TheController.PreviousPosition, position);
    }

    public void WalkToRandomPoints(NpcController TheController, Vector2[] RandomPostions) 
    {
        int randPos = Random.RandomRange(0, RandomPostions.Length);

        TheController.FindNewpath(TheController.PreviousPosition, RandomPostions[randPos]);
    }

}
