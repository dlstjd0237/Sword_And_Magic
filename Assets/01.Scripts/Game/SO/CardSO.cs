using UnityEngine;

public class CardSO : ScriptableObject
{
    public string CardName;
    public Sprite CardSprite;
    [TextArea] public string CardDescription;
}
