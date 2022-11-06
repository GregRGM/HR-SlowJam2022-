using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="Int Variable", menuName ="Scriptable Objects / Variables")]
public class IntVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public int IntialValue;

    public int RuntimeValue;

    public void OnBeforeSerialize()
    {
        
    }

    public void OnAfterDeserialize()
    {
        RuntimeValue = IntialValue;
    }
}

[Serializable]
public class IntReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public IntVariable Variable;

    public float Value
    {
        get { return UseConstant ? ConstantValue : Variable.RuntimeValue; }
    }
}
