using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IControllable
{
    GameObject gameObject { get; }
    void youveBeenTouched();
    void MoveTo(Vector3 destination);
    void MoveToWorldPoint(Touch touchPoint, Vector3 destination);
}
