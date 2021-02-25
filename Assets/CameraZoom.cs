using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    private float zoomFactor = 3f;
    private float zoomLerpSpeed = 10;
    private float initialDistance;
    Vector3 touchstart;

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

        float touchDataUpdated;

        if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled
                    || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
        {
            return;
        }


        if (touchZero.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Began)
        {
            initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
        }
        else
        {
            Debug.Log("Doing camera zoom stuff");
            touchDataUpdated = Vector2.Distance(touchZero.position, touchOne.position);
            touchDataUpdated = touchDataUpdated - initialDistance;
            Debug.Log("touch data updated = " + touchDataUpdated);
            Debug.Log("zoom factor is: " + zoomFactor);
            targetZoom -= touchDataUpdated * zoomFactor;
            Debug.Log("target zoom is: " + targetZoom);
            targetZoom = Mathf.Clamp(targetZoom, 4.5f, 8f);     //clamp stops it doing its best impression of team rocket when they lose
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
        }



        //camera strafe
        if(Input.GetMouseButtonDown(0))
        {
            touchstart = cam.ScreenToWorldPoint(Input.GetTouch(0).position);

        }
        if (Input.GetMouseButton(0))
        {
            Vector3 camDir = touchstart - cam.ScreenToWorldPoint(Input.GetTouch(0).position);
            cam.transform.position += camDir;
        }
    }
}
