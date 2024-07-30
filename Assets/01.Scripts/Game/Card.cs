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

    public Card(VisualElement root)
    {
        _root = root;
        _cardContain = root.Q<VisualElement>("card");
        _namelabel = root.Q<Label>("card_name-label");
        _description = root.Q<Label>("card_description-label");
        _spriteBox = root.Q<VisualElement>("card_image_contain-box");
    }

    public void CardSet(string titleName, string description, Sprite icon)
    {
        TitleName = titleName;
        Description = description;
        SpriteImage = icon;
    }
}
