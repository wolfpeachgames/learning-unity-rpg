using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This annotation allows you to create this object from Unitys menu
[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    public bool initialValue;

    //[HideInInspector]
    public bool RuntimeValue;

    public void OnBeforeSerialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnAfterDeserialize() { }
}