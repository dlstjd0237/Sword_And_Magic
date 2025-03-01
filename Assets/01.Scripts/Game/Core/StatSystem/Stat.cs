using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat
{
    [SerializeField] private int _baseValue;

    public Action<int> ValueChangeEvent;
    public List<int> modifiers;

    public int GetValue()
    {
        int finalValue = _baseValue;
        for (int i = 0; i < modifiers.Count; ++i)
        {
            finalValue += modifiers[i];
        }

        return finalValue;
    }

    public void AddModifier(int value)
    {
        if (value != 0)
        {
            modifiers.Add(value);
            ValueChangeEvent?.Invoke(GetValue());
        }
    }

    public void RemoveModifier(int value)
    {
        if (value != 0)
        {
            modifiers.Remove(value);
            ValueChangeEvent?.Invoke(GetValue());
        }
    }
    public void SetDefaultValue(int value)
    {
        _baseValue = value;
    }
}