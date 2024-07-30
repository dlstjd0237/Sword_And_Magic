using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CardChoiceUI : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset _treeAsset;

    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _contain;

    private List<Card> _addCardList;
    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _contain = _root.Q<VisualElement>("contain-box");
        _addCardList = new List<Card>();
        for (int i = 0; i < 3; ++i)
        {
            AddCard();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            _contain.ToggleInClassList("on");
        if (Input.GetKeyDown(KeyCode.X))
            OpenCard();

    }

    private void AddCard()
    {
        var cardTree = _treeAsset.Instantiate().Q<VisualElement>();

        Card card = new Card(cardTree);
        _addCardList.Add(card);
        _contain.Add(cardTree);
    }

    public void OpenCard()
    {
        StartCoroutine("OpenCardCorutine");
    }
    private IEnumerator OpenCardCorutine()
    {
        for (int i = 0; i < 3; ++i)
        {
            _addCardList[i].CardContain.ToggleInClassList("on");
            yield return new WaitForSeconds(0.2f);
        }
    }
}
