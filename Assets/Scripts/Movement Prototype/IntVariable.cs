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
    public int ConstantValue;
    public IntVariable Variable;

    public int Value
    {
        get { return UseConstant ? ConstantValue : Variable.RuntimeValue; }
    }

    public void SetValue(int newvalue)
    {
        if (UseConstant)
        {
            ConstantValue = newvalue;
        }
        else
        {
            Variable.RuntimeValue = newvalue;
        }
    }
}
