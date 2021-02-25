using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;
    private float zoomLerpSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {

        //Get the two touches and store them
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        //store the initial positions
        Vector2 touchZeroOriginalPosition = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOneOriginalPosition = touchOne.position - touchOne.deltaPosition;
        
        float touchDataInitial = Vector2.Distance(touchZero.position, touchOne.position);
        float touchDataUpdated;

        if (touchZero.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Began)
        {
            Debug.Log("Doing camera zoom stuff");
            touchDataUpdated = Vector2.Distance(touchZero.position, touchOne.position);
            touchDataUpdated = touchDataUpdated/1000;
            Debug.Log("touch data updated = " + touchDataUpdated);
            targetZoom -= touchDataUpdated * zoomFactor;
            Debug.Log("target zoom is: " + targetZoom);
            targetZoom = Mathf.Clamp(targetZoom, 4.5f, 8f);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
        }
    }
}
