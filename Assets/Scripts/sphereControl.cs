using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereControl : MonoBehaviour, IControllable
{
    private Vector3 drag_position;

    public void MoveTo(Vector3 destination)
    {
        drag_position = destination;
    }
    public void MoveToWorldPoint(Touch touchPoint, Vector3 destination)
    {
        
        Vector3 touchedLoc = Camera.main.ScreenToWorldPoint(new Vector3(touchPoint.position.x, touchPoint.position.y, 10));

        transform.position = Vector3.Lerp(transform.position, touchedLoc, Time.deltaTime);

        drag_position = destination;
    }

    public void youveBeenTouched()
    {
        //transform.position += Vector3.right;
        Debug.Log("touch detected");
    }

    // Start is called before the first frame update
    void Start()
    {
        drag_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,drag_position,0.05f);
    }
}
