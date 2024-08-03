using System;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Stat/Player")]
public class PlayerStat : CharactersStat
{
    protected override void OnEnable()
    {
        base.OnEnable();

        Type playerStatType = typeof(PlayerStat);

        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            string statName = LowerFirstChar(statType.ToString());

            try
            {
                FieldInfo playerStatField = playerStatType.GetField(statName);
                Stat stat = playerStatField.GetValue(this) as Stat;
                _statDictionary.Add(statType, stat);
            }
            catch
            {
                Debug.Log("Error : Stat Don't Add");
            }
        }
    }
    public Stat GetStatByType(StatType statType)
    {
        return _statDictionary[statType];
    }
    private string LowerFirstChar(string input)
    {
        return char.ToLower(input[0]) + input.Substring(1);
    }
}
