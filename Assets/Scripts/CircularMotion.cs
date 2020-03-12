using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CircularMotion : MonoBehaviour
{
    [SerializeField] private float angularSpeed;
    [SerializeField] private float motionRadius;

    [SerializeField] private Vector3 centerPoint;
    
    
    private void Start()
    {
        transform.position = centerPoint + Vector3.back * motionRadius;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            transform.RotateAround(centerPoint,Vector3.up, Time.deltaTime*angularSpeed);
    }
}
