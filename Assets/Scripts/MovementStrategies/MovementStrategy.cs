using UnityEngine;

namespace MovementStrategies
{
    public abstract class MovementStrategy : ScriptableObject
    {
        public abstract void Move(Transform movable, float speed, Vector3 centerPoint);
    }
}
