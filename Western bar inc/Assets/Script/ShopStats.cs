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
    public int CustomerSpeed;
    public TMP_Text CustomerSpeedText;
    public int SearchingSpeed;
    public TMP_Text SearchingSpeedText;

    public int Decoreation;
    public TMP_Text DecorationText;
    public int Seats;
    public TMP_Text SeatsText;
    public int Entertainment;
    public TMP_Text EntertainmentText;



    // Start is called before the first frame update
    void Start()
    {
        MoneyText.text = "$" + Money;
        StaffSpeedText.text = "Staff speed lvl: " + StaffSpeed;
        CustomerSpeedText.text = "Customer speed lvl: " + CustomerSpeed;
        SearchingSpeedText.text = "Searching speed lvl: " + SearchingSpeed;
        DecorationText.text = "Decoration lvl: " + Decoreation;
        SeatsText.text = "Seats lvl: " + Seats;
        EntertainmentText.text = "Entertainment lvl: " + Entertainment; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeMoney(float amount)
    {
        Money += amount;
        MoneyText.text = "$" + Money.ToString();
    }
}
