using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntrestAreas : MonoBehaviour
{
    public List<AreaSpot> SpotList;
}

[System.Serializable]
public class AreaSpot 
{
    [SerializeField]
    private string name;
    [SerializeField]
    private List<Vector2Int> pos;

    public string Name 
    { 
        get { return name; } 
        set { name = value; } 
    }

    public List<Vector2Int> Pos
    {
        get { return pos; }
        set { pos = value; }
    }
}
