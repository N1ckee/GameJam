using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController : MonoBehaviour
{

    public float Speed;

    public float WayPointRad;

    public List<Vector3> WayPoints;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WayPoints != null && WayPoints.Count > 1) 
        { 
            Vector2 wayPoint = new Vector2(WayPoints[0].x, WayPoints[0].y + 0.25f);
            this.transform.position += GetDirection(wayPoint) * Speed * Time.deltaTime;

            if (Vector2.Distance(this.transform.position, wayPoint) < WayPointRad && WayPoints.Count > 1) 
            {
                WayPoints.RemoveAt(0);
            }

        }
    }

    Vector3 GetDirection(Vector2 pos) 
    {
        Vector2 direction = pos - (new Vector2(this.transform.position.x, this.transform.position.y));
        Debug.DrawRay(this.transform.position,direction, Color.green);
        return direction.normalized;
    }
}
