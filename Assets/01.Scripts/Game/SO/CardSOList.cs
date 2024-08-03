using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Card/List")]
public class CardSOList : ScriptableObject
{
    [SerializeField] private List<CardSO> _cardSOList; public List<CardSO> CardListSO { get { return _cardSOList; } }
}
