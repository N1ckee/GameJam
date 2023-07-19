using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewsPapper : MonoBehaviour
{
    public ShopStats TheShopStats;
    public TMP_Text TheText;
    public Saloon MySallon; 
    public List<Transform> spots;
    public List<Saloon> Saloons;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MySallon.Rev = TheShopStats.Earnings;

        foreach (Saloon saloon in Saloons)
        {
            if (saloon.Obj == MySallon.Obj)
            {
                saloon.Rev = MySallon.Rev;
            }
        }

        Saloons.Sort((a, b) => a.Rev.CompareTo(b.Rev));

        for (int i = 0; i < Saloons.Count; i++)
        {
            Saloons[i].Obj.transform.position = spots[i].position;
        }

        TheText.text = "Western Saloon - $" + MySallon.Rev;
    }
}

[System.Serializable]
public class Saloon
{
    [SerializeField]
    private GameObject obj;
    [SerializeField]
    private float rev; 

    public GameObject Obj
    {
        get { return obj; }
        set { obj = value; }
    }

    public float Rev 
    { 
        get { return rev; }
        set { rev = value;}
    }
}
