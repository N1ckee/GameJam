using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Staff : MonoBehaviour
{
    public NpcController Controller;
    public OrderManger orderManger;
    public ShopStats shopStats;
    public TMP_Text TalkingText;
    public float talkingSpeed = 0.08f;
    public bool NothingToDo = true;
    public bool IsTakingOrder = false;


    // Start is called before the first frame update
    void Start()
    {
        orderManger = FindAnyObjectByType<OrderManger>();
        shopStats = FindObjectOfType<ShopStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Speed()
    {
        double newSpeed = 0.8 + (0.22 * shopStats.StaffSpeed);
        Controller.Speed = (float)newSpeed;
    }
}
