using System.Collections;
using EventDispatchers;
using UnityEngine;

public class CameraTranslator : MonoBehaviour
{
   [SerializeField] private float circleTransitionSpeed;
   [SerializeField] private float levelTransitionSpeed;

   private Vector3 startPosition;

   private void Awake()
   {
      startPosition = transform.position;
   }

   private void OnEnable()
   {
      ViewController.OnClick += TranslateToStartPosition;
      PlayerCollisionEventsDispatcher.OnCircleTransition += TranslateToNextCircle;
   }

   private void OnDisable()
   {
      ViewController.OnClick -= TranslateToStartPosition;
      PlayerCollisionEventsDispatcher.OnCircleTransition -= TranslateToNextCircle;
   }

   private void TranslateToNextCircle() => StartCoroutine(TranslateToNextCircleRoutine(Vector3.forward*14,circleTransitionSpeed));

   private void TranslateToStartPosition()
   {
      StopAllCoroutines();
      
      Vector3 distance = startPosition - transform.position;
      StartCoroutine(TranslateToNextCircleRoutine(distance,levelTransitionSpeed));
   }

   IEnumerator TranslateToNextCircleRoutine(Vector3 distance, float speed)
   {
      Vector3 desiredPosition = transform.position + distance;
      
      while ((transform.position - desiredPosition).magnitude>0.01f)
      {
          transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * speed);
          yield return null;
      }
   }
}
