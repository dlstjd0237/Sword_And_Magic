using System.Collections.Generic;
using UnityEngine;

public class CharactersStat : ScriptableObject
{
    public Stat attackDamage;
    public Stat attackSpeed;
    public Stat maxHealth;

    protected Entity _owner;

    protected Dictionary<StatType, Stat> _statDictionary;

    public virtual void SetOwner(Entity owner)
    {
        _owner = owner;
    }

    public void AddStatPoint(StatType stat, int point)
    {
        _statDictionary[stat].AddModifier(point);
    }

    protected virtual void OnEnable()
    {
        _statDictionary = new Dictionary<StatType, Stat>();
    }

    public int GetHealth()
    {
        return maxHealth.GetValue();
    }

}
