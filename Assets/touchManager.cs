using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchManager : MonoBehaviour
{
    private float[] timeTouchBegan;
    private bool[] touchMoved;
    private float tapTimeDuration = 0.25f;
    private float starting_distance_to_selected_object;
    private float initialDistance;
    private Vector3 initialScale;
    public Vector3 min = new Vector3(1f, 1f, 1f);
    public Vector3 max = new Vector3(5f, 5f, 5f);
    float storedFOV = 0;

    IControllable selectedObject;
    // Start is called before the first frame update
    void Start()
    {
        GameObject cameraPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        cameraPlane.transform.position = new Vector3(transform.position.x, Camera.main.transform.position.y,transform.position.z);
        cameraPlane.transform.up = (Camera.main.transform.position - cameraPlane.transform.position).normalized;
        cameraPlane.layer = 8;
        timeTouchBegan = new float[5];
        touchMoved = new bool[10];
        storedFOV = Camera.main.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            Debug.DrawRay(raycast.origin, 30 * raycast.direction, Color.red);

            if (Physics.Raycast(raycast, out raycastHit))
            {
                IControllable object_hit = raycastHit.transform.GetComponent<IControllable>();
                if (object_hit != null)
                {
                    selectedObject = object_hit;
                    object_hit.youveBeenTouched();
                    starting_distance_to_selected_object = Vector3.Distance(Camera.main.transform.position, raycastHit.transform.position);
                }
                Debug.Log("Hit " + raycastHit.collider.name);
            }
        }
        //assume selected object is not null, the drag code is located here
        switch(Input.touches[0].phase)
        {
            case TouchPhase.Began:
                selectedObject.gameObject.transform.GetComponent<Renderer>().material.color = Color.red;
                break;
            case TouchPhase.Moved:
                Ray new_position_ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                selectedObject.MoveToWorldPoint(Input.touches[0], new_position_ray.GetPoint(starting_distance_to_selected_object));
                //selectedObject.MoveTo(new_position_ray.GetPoint(starting_distance_to_selected_object));
                break;
            case TouchPhase.Ended:

                break;
        }
        //follow floor, is being interfered with by another method
        if (selectedObject.gameObject.transform.name == "SphereTwo")
        {
            Ray newPositionSphere2Ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit[] hitRay = Physics.RaycastAll(newPositionSphere2Ray);
            int groundMask = LayerMask.NameToLayer("Ground");

            foreach(RaycastHit hit in hitRay)
            {
                Debug.Log(groundMask);
                if(hit.transform.gameObject.layer == groundMask)
                {
                    Debug.Log("ground detected");
                    selectedObject.MoveTo(hit.point);
                }
            }


            //tried this for follow floor, didnt work

            //New ray
            //Ray raycastPlane = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            //RaycastHit RCHit2;
            //if (Physics.Raycast(raycastPlane, out RCHit2))
            //{
            //    IControllable object_hit = RCHit2.transform.GetComponent<IControllable>();
            //    if (object_hit.gameObject.transform.name.Equals("Plane"))
            //    {
            //        Debug.Log("!!! Sphere two destination is = " + object_hit.gameObject.transform.localPosition);
            //        selectedObject.MoveTo(object_hit.gameObject.transform.localPosition);
            //    }
            //}
        }

        
        //if (Input.touchCount == 2)          //for some reason this only works when the sphere or cylinder are selected. not sure why
        //{
        //    Debug.Log("camera zoom begin");
        //    //Get the two touches and store them
        //    Touch touchZero = Input.GetTouch(0);
        //    Touch touchOne = Input.GetTouch(1);

        //    //store the initial positions
        //    Vector2 touchZeroOriginalPosition = touchZero.position - touchZero.deltaPosition;
        //    Vector2 touchOneOriginalPosition = touchOne.position - touchOne.deltaPosition;

        //    if (touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
        //    {
        //        return;
        //    }

        //    if (touchZero.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Began)
        //    {
        //        Debug.Log("Doing camera zoom stuff");
        //        initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
        //        Camera.main.fieldOfView -= 20f / 8;
        //        if(Camera.main.fieldOfView < 10f)
        //        {
        //            Camera.main.fieldOfView = 10f;
        //        }
        //        if(Camera.main.fieldOfView > 20f)
        //        {
        //            Camera.main.fieldOfView = 20f;
        //        }
        //        StartCoroutine(DelayForFive());     //delay for five works only for resetting the camera FOV
                
        //    }
        //}

                //SCALING CODE works but badly, its very aggressive !! HAS SINCE BEEN RECITIFIED
        if (selectedObject.gameObject.name == "Sphere")
        {
            if(Input.touchCount == 2)
            {
                Debug.Log("Doing scale");
                //Get the two touches and store them
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);


                if(touchZero.phase == TouchPhase.Ended || touchZero.phase == TouchPhase.Canceled
                    || touchOne.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Canceled)
                { 
                    return; 
                }

                if (touchZero.phase == TouchPhase.Ended || touchOne.phase == TouchPhase.Began)
                {
                    initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    initialScale = selectedObject.gameObject.transform.localScale;
                }

                else
                {
                    var currentDistance = Vector2.Distance(touchZero.position, touchOne.position);

                    if (Mathf.Approximately(initialDistance, 0)) return;

                    var factor = currentDistance / initialDistance;

                    selectedObject.gameObject.transform.localScale = initialScale * factor;
                }

                ////get the touches magnitudes
                //float originTouchDeltaMagnitude = (touchZeroOriginalPosition - touchOneOriginalPosition).magnitude;
                //float touchDeltaMagnitude = (touchZero.position - touchOne.position).magnitude;

                ////perform the final calculations and apply them to the selectedObject ONLY
                //float deltaMagnitudeDifference = originTouchDeltaMagnitude - touchDeltaMagnitude;
                //selectedObject.gameObject.transform.localScale = new Vector3(deltaMagnitudeDifference, deltaMagnitudeDifference, deltaMagnitudeDifference) * Time.deltaTime;
            }
        }
        //this causes the cube to rotate at the point of my thumb, does something but its definitely not right
        if (selectedObject.gameObject.name == "Cube")
        {
            Touch touchZero = Input.GetTouch(0);

            Vector3 touchZeroOriginalPosition = touchZero.position - touchZero.deltaPosition;

            Debug.Log(touchZeroOriginalPosition + ": is the touch zero original position");
            selectedObject.gameObject.transform.Rotate(Camera.main.transform.position);
        }
        
        if (selectedObject.gameObject.name == "Cylinder")
        {
            Touch touchZero = Input.GetTouch(0);
            var delta = touchZero.position;
            Vector3 originPoint = Camera.main.transform.position;

            selectedObject.gameObject.transform.Rotate(Vector3.up * delta.x * Time.deltaTime);

        }

        foreach (Touch touch in Input.touches)
        {
            int fingerCounter = touch.fingerId;

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("The following finger index has been detected " + fingerCounter.ToString());
                timeTouchBegan[fingerCounter] = Time.time;
                touchMoved[fingerCounter] = false;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("The finger with the following index moved" + fingerCounter.ToString());
                touchMoved[fingerCounter] = true;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                float timeOfTap = Time.time - timeTouchBegan[fingerCounter];
                Debug.Log("The finger #" + fingerCounter.ToString() + " remained on the screen for " + timeOfTap.ToString());
                if (timeOfTap <= tapTimeDuration && touchMoved[fingerCounter] == false)
                {
                    Debug.Log("Tap was detected from finger " + fingerCounter.ToString() + " at: " + touch.position.ToString());
                }
                else
                {
                    Debug.Log("a drag/hold movement was detected from finger " + fingerCounter.ToString() + " at: " + touch.position.ToString());
                }
            }
        }
    }
    private IEnumerator DelayForFive()
    {
        yield return new WaitForSeconds(5f);
        Camera.main.fieldOfView = storedFOV;
    }
}
