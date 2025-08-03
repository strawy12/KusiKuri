using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventParamData
{
    public EventType eventType;
    public int intParam;
    public float floatParam;
    public string stringParam;
    public bool boolParam;
}

public static class EventParamDataUtils
{
    public static bool TryParseEventData<T>(this EventParamData data, out T parseData) where T : EventParamData
    {
        parseData = null;
        if (data is T)
        {
            parseData = data as T;
            return true;
        }

        else
        {
            return false;
        }
    }
}

public class LeftClickEventData : EventParamData
{
    public List<UnityEngine.EventSystems.RaycastResult> hits;
}
