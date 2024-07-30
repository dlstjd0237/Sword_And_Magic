using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainScreen : MonoBehaviour
{
    [SerializeField] private VisualTreeAsset _registerTree, _loginTree;

    private UIDocument _doc;
    private VisualElement _root;
    private VisualElement _btnBar;
    private VisualElement _popupPanel;

    public NetworkEventChannelSO registerChannel, postResultChannel, loginChannel;
    public SetDataEventCahnnelSO tokenChannel,gameStartChannel;

    private RegisterScreen _registerScreen;
    private LoginScreen _loginScreen;


    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _registerScreen = new RegisterScreen(this);
        _loginScreen = new LoginScreen(this);
    }

    private void Start()
    {
        gameStartChannel.OnRaiseEvent.AddListener(HandleGameStart);
    }

 
    private void HandleGameStart(DataEvent evt)
    {
        _btnBar.AddToClassList("hide");
    }

    private void OnEnable()
    {
        _root = _doc.rootVisualElement;

        _btnBar = _root.Q<VisualElement>("btn_bar");
        _popupPanel = _root.Q<VisualElement>("popup_panel");
        _root.Q<Button>("btn_register").clicked += HandleRegisterBtnCliek;
        _root.Q<Button>("btn_login").clicked += HandleLoginBtnCliek;

    }

    private void OnDestroy()
    {
        _loginScreen.Dispose();
        _registerScreen.Dispose();
    }

    private void HandleLoginBtnCliek()
    {
        var template = _loginTree.Instantiate();
        _popupPanel.Clear();
        _popupPanel.Add(template);

        _loginScreen.Initialize(template);

        _popupPanel.AddToClassList("on");
    }

    private void HandleRegisterBtnCliek()
    {
        var template = _registerTree.Instantiate();
        _popupPanel.Clear();
        _popupPanel.Add(template);

        _registerScreen.Initialize(template);

        _popupPanel.AddToClassList("on");
    }
    public void ClosePopupPanel()
    {
        _popupPanel.RemoveFromClassList("on");
        _popupPanel.RegisterCallback<TransitionEndEvent>(ClearPopupPanel);
    }

    private void ClearPopupPanel(TransitionEndEvent evt)
    {
        _popupPanel.Clear();
        _popupPanel.UnregisterCallback<TransitionEndEvent>(ClearPopupPanel);
    }
}
