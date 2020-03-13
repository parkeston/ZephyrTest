using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CircularMotion : MonoBehaviour
{
    [SerializeField] private float angularSpeed;
    [SerializeField] private float motionRadius;

    [SerializeField] private Transform[] centerPoints;

    private bool canGoToNextCircle;
    private bool isTranslatingToNextCircle;

    private int currentCircle;
    private Vector3 centerPoint => centerPoints[currentCircle].position;
    private Vector3 startPoint => centerPoint + Vector3.back * motionRadius + Vector3.up;

    private Vector3 stopPoint;

    private void Start()
    {
        transform.position =startPoint;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0)&&!isTranslatingToNextCircle)
            transform.RotateAround(centerPoint, Vector3.up, Time.deltaTime * angularSpeed);
        else if (canGoToNextCircle && !isTranslatingToNextCircle)
        {
            //fix: get radius of circle base
            Vector3 distance = -centerPoint;
            currentCircle++;
            distance += centerPoint;
            Camera.main.GetComponent<CameraTranslator>().TranslateToNextCircle(distance);
            
            StartCoroutine(TranslateToNextCircleBaseRoutine(startPoint));
        }
    }

    //todo: move logic of translation to another circle outside of the scope of this script?
    IEnumerator TranslateToNextCircleBaseRoutine(Vector3 desiredPosition)
    {
        isTranslatingToNextCircle = true;

        transform.position = stopPoint;
        transform.rotation = Quaternion.identity;
        while ((transform.position - desiredPosition).magnitude > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * 5);
            yield return null;
        }

        isTranslatingToNextCircle = false;
        canGoToNextCircle = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        canGoToNextCircle = true;
        stopPoint = other.transform.position+Vector3.up;
    }

    private void OnTriggerExit(Collider other)
    {
        canGoToNextCircle = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        print("collision!");
        Destroy(gameObject);
    }
}