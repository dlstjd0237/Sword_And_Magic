using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(menuName = "SO/Card/CardSO")]
public class CardSO : ScriptableObject
{
    public string CardName;
    public Sprite CardSprite;
    [TextArea] public string CardDescription;
    public List<StatPieceSO> PieceSOList;
}
