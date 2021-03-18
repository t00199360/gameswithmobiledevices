using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoFingerRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void LateUpdate()
	{
		float pinchAmount = 0;
		Quaternion desiredRotation = transform.rotation;

		FingerRotation.Calculate();

		if (Mathf.Abs(FingerRotation.pinchDistanceDelta) > 0)
		{ // zoom
			pinchAmount = FingerRotation.pinchDistanceDelta;
		}

		if (Mathf.Abs(FingerRotation.turnAngleDelta) > 0)
		{ // rotate
			Vector3 rotationDeg = Vector3.zero;
			rotationDeg.z = -FingerRotation.turnAngleDelta;
			desiredRotation *= Quaternion.Euler(rotationDeg);
		}


		// not so sure those will work:
		transform.rotation = desiredRotation;
		transform.position += Vector3.forward * (pinchAmount * 2);
	}
}
