using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
public class CardChoiceUI : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset _treeAsset;
    [SerializeField] private CardSOList _soList;
    [SerializeField] private Player _player;
    [SerializeField] private SetDataEventCahnnelSO _gameTimeChannel;


    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _contain;
    private float _timer;

    private List<Card> _addCardList;
    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
    }
    private void Start()
    {
        _gameTimeChannel.OnRaiseEvent.AddListener(HandleGameTimeChanged);
    }
    private void HandleGameTimeChanged(DataEvent evt)
    {
        FloatDataEvent floatEvt = evt as FloatDataEvent;
        _timer = floatEvt.data % 20;
        if (_timer > 19.99f)
        {
            _timer = 0;
            OpenCard();
        }
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

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("??");
            OpenCard();
        }

    }

    private void AddCard()
    {
        var cardTree = _treeAsset.Instantiate().Q<VisualElement>();
        cardTree.RegisterCallback<ClickEvent>(evt =>
        {
            GameEventBus.Publish(GameEventBusType.Start);
            StartCoroutine(OpenCardCorutine(true));
        });

        Card card = new Card(cardTree, _player);
        _addCardList.Add(card);
        _contain.Add(cardTree);
    }

    public void OpenCard()
    {
        GameEventBus.Publish(GameEventBusType.Stop);
        _contain.ToggleInClassList("on");
        for (int i = 0; i < 3; ++i)
        {
            List<CardSO> cardList = _soList.CardListSO;
            CardSO card = cardList[Random.Range(0, cardList.Count)];
            _addCardList[i].CardSet(card);
        }
        StartCoroutine(OpenCardCorutine());
    }


    private IEnumerator OpenCardCorutine(bool shutdown = false)
    {
        if (shutdown)
        {
            for (int i = 0; i < 3; ++i)
            {
                _addCardList[i].CardContain.pickingMode = PickingMode.Ignore;
            }
        }


        for (int i = 0; i < 3; ++i)
        {
            _addCardList[i].CardContain.ToggleInClassList("on");
            yield return new WaitForSeconds(0.2f);
        }
        if (shutdown)
        {
            _contain.ToggleInClassList("on");
        }
        else
        {
            for (int i = 0; i < 3; ++i)
            {
                _addCardList[i].CardContain.pickingMode = PickingMode.Position;
            }
        }

    }
}
