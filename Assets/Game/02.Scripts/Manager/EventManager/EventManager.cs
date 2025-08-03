using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    private static Dictionary<string, Action<EventParamData>> eventDictionary = new Dictionary<string, Action<EventParamData>>();

    public static void StartListening<T>(T eventName, Action<EventParamData> listener) where T : Enum
    {
        Action<EventParamData> thisEvent;
        string key = $"{typeof(T).ToString()}_{eventName.ToString()}";
        {
            if (eventDictionary.TryGetValue(key, out thisEvent))
            {
                thisEvent += listener;
                eventDictionary[key] = thisEvent;
            }

            else
            {
                eventDictionary.Add(key, listener);
            }
        }
    }

    public static void StopListening<T>(T eventName, Action<EventParamData> listener) where T : Enum
    {
        Action<EventParamData> thisEvent;
        string key = $"{typeof(T).ToString()}_{eventName.ToString()}";

        if (eventDictionary.TryGetValue(key, out thisEvent))
        {
            thisEvent -= listener;
            eventDictionary[key] = thisEvent;
        }

        else
        {
            eventDictionary.Remove(key);
        }
    }
    public static void StopAllListening<T>(T eventName) where T : Enum
    {
        string key = $"{typeof(T).ToString()}_{eventName.ToString()}";
        eventDictionary.Remove(key);
    }
    public static void TriggerEvent<T>(T eventName, EventParamData param = null) where T : Enum
    {
        Action<EventParamData> thisEvent;
        string key = $"{typeof(T).ToString()}_{eventName.ToString()}";

        if (eventDictionary.TryGetValue(key, out thisEvent))
        {
            thisEvent?.Invoke(param);   
        }
    }

}