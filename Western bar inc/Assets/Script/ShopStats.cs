using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopStats : MonoBehaviour
{
    public float Earnings;
    public float Money; 
    public TMP_Text MoneyText;

    public int StaffSpeed;
    public TMP_Text StaffSpeedText;
    public TMP_Text StaffSpeedButton;
    public int CustomerSpeed;
    public TMP_Text CustomerSpeedText;
    public TMP_Text CustomerSpeedButton;
    public int SearchingSpeed;
    public TMP_Text SearchingSpeedText;
    public TMP_Text SearchingSpeedButton;

    public int Decoration;
    public TMP_Text DecorationText;
    public TMP_Text DecorationButton;
    public int Seats;
    public TMP_Text SeatsText;
    public TMP_Text SeatsButton;
    public int Entertainment;
    public TMP_Text EntertainmentText;
    public TMP_Text EntertainmentButton;



    // Start is called before the first frame update
    void Start()
    {
        MoneyText.text = "$" + Money;
        StaffSpeedText.text = "Staff speed lvl: " + StaffSpeed;
        CustomerSpeedText.text = "Customer speed lvl: " + CustomerSpeed;
        SearchingSpeedText.text = "Searching speed lvl: " + SearchingSpeed;
        DecorationText.text = "Decoration lvl: " + Decoration;
        SeatsText.text = "Seats lvl: " + Seats;
        EntertainmentText.text = "Entertainment lvl: " + Entertainment;

        ChangeStaffSpeed();
        ChangeCustomerSpeed();
        ChangeSearchingSpeed();
        ChangeDecoreation();
        ChangeSeats();
        ChangeEntertainment();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeStaffSpeed()
    {
        int Cost = (3 + (5 * StaffSpeed));
        if (Money >= Cost)
        {
            if (StaffSpeed <= 10)
            {
                ChangeMoney(-Cost);
                StaffSpeed++;
                StaffSpeedText.text = "Staff speed lvl: " + StaffSpeed;
                Cost = (2 + (5 * StaffSpeed));

                Staff[] Staffs = GetComponents<Staff>();

                foreach (Staff TheStaff in Staffs)
                {
                    TheStaff.Speed();
                }
            }
        }
        StaffSpeedButton.text = "$" + Cost;
    }

    public void ChangeCustomerSpeed()
    {
        int Cost = (2 + (5 * CustomerSpeed));
        if (Money >= Cost)
        {
            if (CustomerSpeed <= 10)
            {
                ChangeMoney(-Cost);
                CustomerSpeed++;
                CustomerSpeedText.text = "Customer speed lvl: " + CustomerSpeed;
                Cost = (2 + (5 * CustomerSpeed));

                Customer[] Customers = GetComponents<Customer>();

                foreach (Customer TheCustomer in Customers)
                {
                    TheCustomer.Speed();
                }
            }
        }
        CustomerSpeedButton.text = "$" + Cost;
    }

    public void ChangeSearchingSpeed()
    {
        int Cost = (2 + (5 * SearchingSpeed));
        if (Money >= Cost)
        {
            if (SearchingSpeed <= 10)
            {
                ChangeMoney(-Cost);
                SearchingSpeed++;
                SearchingSpeedText.text = "Customer speed lvl: " + SearchingSpeed;
                Cost = (2 + (5 * SearchingSpeed));

                DrinkMaking[] AllDrinks = GetComponents<DrinkMaking>();

                foreach (DrinkMaking TheDrink in AllDrinks)
                {
                    MakingPosition[] EveryAction = TheDrink.makingPositionList;

                    foreach (MakingPosition Action in EveryAction)
                    {
                        if (Action.ActionName == "Looking")
                        {
                            double change = 6 - (SearchingSpeed * 0.5);
                            Action.TimeToMake = (float)change; 
                        }
                    }
                }
            }
        }
        SearchingSpeedButton.text = "$" + Cost;
    }

    public void ChangeDecoreation()
    {
        int Cost = (2 + (5 * Decoration));
        if (Money >= Cost)
        {
            if (Decoration <= 10)
            {
                ChangeMoney(-Cost);
                Decoration++;
                DecorationText.text = "Decoration lvl: " + Decoration;
                Cost = (2 + (5 * Decoration));
            }
        }
        DecorationButton.text = "$" + Cost;
    }

    public void ChangeSeats()
    {
        int Cost = (2 + (5 * Seats));
        if (Money >= Cost)
        {
            if (Seats <= 10)
            {
                ChangeMoney(-Cost);
                Seats++;
                SeatsText.text = "Seats lvl: " + Seats;
                Cost = (2 + (5 * Seats));
            }
        }
        SeatsButton.text = "$" + Cost;
    }

    public void ChangeEntertainment()
    {
        int Cost = (3 + (5 * Entertainment));
        if (Money >= Cost)
        {
            if (Entertainment <= 10)
            {
                ChangeMoney(-Cost);
                Entertainment++;
                EntertainmentText.text = "Entertainment lvl: " + Entertainment;
                Cost = (2 + (5 * Entertainment));
            }
        }
        EntertainmentButton.text = "$" + Cost;
    }

    public void ChangeMoney(float amount)
    {
        Money += amount;
        MoneyText.text = "$" + Money.ToString();
        if (amount > 0)
        {
            Earnings += amount;
        }
    }
}
