using UnityEngine;

namespace MovementStrategies
{
    [CreateAssetMenu(menuName = "CircularMovementStrategy",fileName = "NewCircularMovementStrategy",order = 1)]
    public class CircularMovementStrategy : MovementStrategy
    {
        public override void Move(Transform movable, float speed, Vector3 centerPoint)
        {
            movable.RotateAround(centerPoint,Vector3.up, speed*Time.deltaTime);
        }
    }
}
