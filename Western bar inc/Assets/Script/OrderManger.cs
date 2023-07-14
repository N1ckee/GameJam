using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class OrderManger : MonoBehaviour
{

    public List<Customer> Orders;
    public List<Staff> StaffOrders;

    public bool IsTakeingCustomer = false;

    public string[] DrinkMenu; 
    public float PayTime = 2;
    public float ServTime = 5;

    private float RealPayTime;
    private float RealServTime;
    private bool OneTime = false;

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

    // Start is called before the first frame update
    void Start()
    {
        RealPayTime = PayTime;
        RealServTime = ServTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTakeingCustomer && Orders.Count == 0)
        {
            Orders.Add(Queing[0]);
            Queing.Remove(Queing[0]);

            Orders[0].FindNewpath(Orders[0].Controller.LastWayPoint, PayArea[0]);
        }

        if (AvailableStaff.Count > 0 && Queing.Count > 0) 
        {
            StaffOrders.Add(AvailableStaff[0]);
            AvailableStaff.Remove(AvailableStaff[0]);

            StaffOrders[0].FindNewpath(StaffOrders[0].Controller.LastWayPoint, StaffPayArea[0]);
            WaitingForStaff = true;
        }

        if (WaitingForStaff) 
        { 
            if (StaffOrders[0].Controller.WayPoints.Count <= 1) 
            {
                IsTakeingCustomer = true;
                WaitingForStaff = false;
            }
        }

        if (IsTakeingCustomer)
        {
            if (StaffOrders[0].Controller.WayPoints.Count <= 1 && Orders[0].Controller.WayPoints.Count <= 1)
            {
                if (PayTime <= 0 && !OneTime)
                {
                    int rand = Random.Range(0, Orders[0].favoriteDrinks.Length);
                    StartCoroutine(Orders[0].SaySomething(Orders[0].favoriteDrinks[rand] + " Please"));
                    StartCoroutine(PrepareDrink(StaffOrders[0], Orders[0], Orders[0].favoriteDrinks[rand]));
                    OneTime = true;
                    IsTakeingCustomer = false;
                }

                PayTime -= Time.deltaTime;
            }
        }
    }

    public IEnumerator PrepareDrink(Staff staff, Customer theCustomer, string drink) 
    {
        yield return new WaitForSeconds(1);
        // Find drink
        int randDrink = Random.Range(0, Shelfs.Count);

        staff.FindNewpath(staff.Controller.LastWayPoint, Shelfs[randDrink]);

        bool IsServing = true;

        while (IsServing) 
        {
            if (staff.Controller.WayPoints.Count <= 1)
            {
                yield return new WaitForSeconds(5);
                theCustomer.FindNewpath(theCustomer.Controller.LastWayPoint, ServArea[0]);
                staff.FindNewpath(staff.Controller.LastWayPoint, StaffServArea[0]);
                IsServing = false;
                break;
            }

            yield return new WaitForSeconds(3);
            int randWaitingArea = Random.Range(0, WaitAreas.Count);
            theCustomer.FindNewpath(theCustomer.Controller.LastWayPoint, WaitAreas[randWaitingArea]);
        }

        // Serv Drink

        bool GettingDrink = true;
        StartCoroutine(Orders[0].SaySomething("Thank you for the " + drink + "!"));
        theCustomer.FindNewpath(theCustomer.Controller.LastWayPoint, Seats[0]);

    }

    public void WantsToQue(Customer theCustomer) 
    { 
        if (QueAvailability()) 
        { 
            Queing.Add(theCustomer);
            theCustomer.FindNewpath(theCustomer.Controller.LastWayPoint, QueSpots[Queing.Count - 1]);
        }
    }

    public bool QueAvailability() 
    {
        if (QueSpots.Count > Queing.Count)
        {
            return true;
        }
        else
            return false;
    }

}
