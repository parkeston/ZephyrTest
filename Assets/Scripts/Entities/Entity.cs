using MovementStrategies;
using UnityEngine;

namespace Entities
{
    public class Entity : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float speed;
        [SerializeField] protected MovementStrategy movementStrategy;

        [Header("Position range")]
        [SerializeField] private float minPercentOffsetFromCenter;
        [SerializeField] private float maxPercentOffsetFromCenter;
   
        private int movementDirection;
        private Vector3 centerPoint;
    
        protected void Move() => movementStrategy.Move(transform, movementDirection * speed, centerPoint);

        public void Locate(int movementDirection, Vector3 centerPoint, Vector3 offset)
        {
            this.movementDirection = movementDirection;
            this.centerPoint = centerPoint;
        
            float offsetPercent = Random.Range(minPercentOffsetFromCenter, maxPercentOffsetFromCenter);
            transform.position = centerPoint + Vector3.Scale(offset, new Vector3(offsetPercent, 1, offsetPercent));

            Vector3 startLookPoint = centerPoint;
            startLookPoint.y = transform.position.y;
            transform.LookAt(startLookPoint, Vector3.up);
      
            //enable update calls, which called from derived classes
            if (movementStrategy != null)
                enabled = true;
        }
    }
}
