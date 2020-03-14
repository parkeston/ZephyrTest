using System;
using UnityEngine;

namespace EventDispatchers
{
    public class PlayerCollisionEventsDispatcher : MonoBehaviour
    {
        public static event Action OnPlayerIsDead;
        public static event Action OnCircleTransition;

        private bool inExitZone;

        private void OnEnable()
        {
            InputEventsDispatcher.OnPointerUpEvent += CheckForCircleTransition;
        }

        private void OnDisable()
        {
            InputEventsDispatcher.OnPointerUpEvent -= CheckForCircleTransition;
        }

        private void CheckForCircleTransition()
        {
            if (inExitZone)
            {
                inExitZone = false;
                OnCircleTransition?.Invoke();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            inExitZone = true;
        }
    
        private void OnTriggerExit(Collider other)
        {
            inExitZone = false;
        }
    
        private void OnCollisionEnter(Collision other)
        {
            OnPlayerIsDead?.Invoke();
            Destroy(gameObject);
        }
    }
}
