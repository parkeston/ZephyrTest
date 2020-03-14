using UnityEngine;

namespace Entities
{
    public class Obstacle : Entity
    {
        [Header("Weights")]
        [SerializeField] private int weight;
        public int Weight => weight;

        private void Update()
        {
            Move();
        }
    }
}
