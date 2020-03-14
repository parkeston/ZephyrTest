using UnityEngine;

namespace Entities
{
    public class Player : Entity
    { 
        private void Update()
        {
            if (Input.GetMouseButton(0))
                Move();
        }
    }
}