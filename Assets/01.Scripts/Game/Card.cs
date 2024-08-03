using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class Card
{
    private VisualElement _root;
    private VisualElement _cardContain;
    public VisualElement CardContain => _cardContain;

    private Label _namelabel;
    private Label _description;
    private VisualElement _spriteBox;
    private Entity _owner;
    private CardSO _currentCardSO;
    public CardSO CurrentCardSO
    {
        get => _currentCardSO;
        set
        {
            _currentCardSO = value;
            TitleName = value.CardName;
            Description = value.CardDescription;
            SpriteImage = value.CardSprite;
        }
    }

    public string TitleName
    {
        set
        {
            _namelabel.text = value;
        }
    }
    public string Description
    {
        set
        {
            _description.text = value;
        }
    }
    public Sprite SpriteImage
    {
        set
        {
            _spriteBox.style.backgroundImage = new StyleBackground(value);
        }
    }

    public Card(VisualElement root, Entity entity)
    {
        _owner = entity;

        _root = root;
        _cardContain = root.Q<VisualElement>("card");
        _namelabel = root.Q<Label>("card_name-label");
        _description = root.Q<Label>("card_description-label");
        _spriteBox = root.Q<VisualElement>("card_image_contain-box");

    }

    public void CardSet(CardSO so)
    {
        CurrentCardSO = so;
        _cardContain.UnregisterCallback<ClickEvent>(HandleClickEvent);
        _cardContain.RegisterCallback<ClickEvent>(HandleClickEvent);
    }

    private void HandleClickEvent(ClickEvent evt)
    {
        var pieceList = _currentCardSO.PieceSOList;
        Debug.Log(pieceList.Count);
        PlayerStat stat = _owner.Stat as PlayerStat;
        for (int i = 0; i < pieceList.Count; ++i)
        {
            StatPiece piece = pieceList[i].Piece;
            stat.GetStatByType(piece.statType).AddModifier(piece.amount);
        }
    }
}
