using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EventDriven
{
    public static class Extensions
    {
        public static void StartListening<T>(this MonoBehaviour mn) where T : struct
        {
            EventManager.AddListener(new T(), mn.GetComponent<IEventListener<T>>());
        }

        public static void StopListening<T>(this MonoBehaviour mn) where T : struct
        {
            EventManager.RemoveListener(new T(), mn.GetComponent<IEventListener<T>>());
        }
    }
}