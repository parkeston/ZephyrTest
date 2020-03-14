using UnityEngine;

namespace MovementStrategies
{
    [CreateAssetMenu(menuName = "RotatingMovementStrategy",fileName = "NewRotatingMovementStrategy",order = 2)]
    public class RotatingMovementStrategy : MovementStrategy
    {
        public override void Move(Transform movable, float speed, Vector3 centerPoint)
        {
            movable.Rotate(Vector3.up,speed*Time.deltaTime);
        }
    }
}
