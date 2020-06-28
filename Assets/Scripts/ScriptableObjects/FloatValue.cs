using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This annotation allows you to create this object from Unitys menu
[CreateAssetMenu]
[System.Serializable]
//public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
public class FloatValue : ScriptableObject
{
    public float initialValue;
    [HideInInspector]
    public float RuntimeValue;

    //public void OnBeforeSerialize() {
    //    RuntimeValue = initialValue;
    //}

    //public void OnAfterDeserialize() { }
}
