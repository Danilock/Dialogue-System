using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventDriven
{
    public class TriggerEventExample : MonoBehaviour, IEventListener<GameEvent>
    {
        [SerializeField] private GameEventsTypes _eventsTypes;

        private void OnEnable()
        {
            this.StartListening<GameEvent>();
        }

        private void OnDisable()
        {
            this.StopListening<GameEvent>();
        }

        public void OnTriggerEvent(GameEvent data)
        {
            Debug.Log(data.EventTypes);
        }

        [ContextMenu("Trigger Test Event")]
        public void TriggerTestEvent()
        {
            EventManager.TriggerEvent(new GameEvent(_eventsTypes));
        }
    }
}