using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchScript : MonoBehaviour, IControllable
{
    public void MoveTo(Vector3 destination)
    {
        throw new NotImplementedException();
    }

    public void MoveToWorldPoint(Touch touchPoint, Vector3 destination)
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }


    void IControllable.youveBeenTouched()
    {
        Debug.Log("Ive been touched");
    }
}
