using UnityEngine;
using TMPro;
public class RankItem : MonoBehaviour
{
    private TextMeshProUGUI _text;

    public string Text
    {
        get => _text.text;
        set
        {
            _text.text = value;
        }
    }

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
