using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTranslator : MonoBehaviour
{
   [SerializeField] private float cameraSpeed;

   public void TranslateToNextCircle(Vector3 distance) => StartCoroutine(TranslateToNextCircleRoutine(distance));

   IEnumerator TranslateToNextCircleRoutine(Vector3 distance)
   {
      Vector3 desiredPosition = transform.position + distance;
      
      while ((transform.position - desiredPosition).magnitude>0.01f)
      {
          transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * cameraSpeed);
          yield return null;
      }
   }
}
