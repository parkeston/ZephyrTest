using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//todo: remove code duplication with player circular motion
public class CircularMotionEnemy : MonoBehaviour
{
    [SerializeField] private float angularSpeed;
    [SerializeField] private float motionRadius;

    [SerializeField] private Transform centerPointTransform;

    private bool canGoToNextCircle;
    private bool isTranslatingToNextCircle;

    private Vector3 centerPoint => centerPointTransform.position;
    private Vector3 startPoint => centerPoint + Vector3.back * motionRadius + Vector3.up;

    private Vector3 stopPoint;

    private void Start()
    {
        transform.position =startPoint;
    }

    private void Update()
    {
        transform.RotateAround(centerPoint, Vector3.up, Time.deltaTime * angularSpeed);
    }
}
