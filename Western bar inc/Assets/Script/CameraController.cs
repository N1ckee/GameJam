using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float movementSpeed;
    public float movementTime;
    public float minx = -5.4f;
    public float maxx = 3.8f;
    public float miny = -1f;
    public float maxy = 3.5f;
    public Camera camera;
    public float viewminimum = 1f;
    public float viewmaximum = 6f;
    public float viewsize = 5f;
    public float zoomspeed = 0.6f;
    public Vector3 newPosition;
    void Start()
    {

        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
    }

    void HandleMovementInput()
    {
        //zoomin in
        if (Input.GetAxis ("Mouse ScrollWheel")> 0)
        {
            viewsize -= zoomspeed;
            viewsize =Mathf.Clamp(viewsize, viewminimum, viewmaximum);
        //zoomin out
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            viewsize += zoomspeed;
            viewsize = Mathf.Clamp(viewsize, viewminimum, viewmaximum);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newPosition += (transform.up * movementSpeed);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newPosition += (transform.up * -movementSpeed);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newPosition += (transform.right * movementSpeed);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newPosition += (transform.right * -movementSpeed);
        }
        newPosition = new Vector3(Mathf.Clamp(newPosition.x, minx, maxx),Mathf.Clamp(newPosition.y, miny, maxy),newPosition.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movementTime);

        
        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize,viewsize, Time.deltaTime * movementTime);
    }

}
