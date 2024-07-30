using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoginScreen : IDisposable
{
    private MainScreen _main;
    private VisualElement _root;
    private readonly string _path = "user_login";
    private TextField _emailField, _passwordField;

    public string Email
    {
        get => _emailField.value;
        set => _emailField.value = value;
    }
    public string Password
    {
        get => _passwordField.value;
        set => _passwordField.value = value;
    }

    public LoginScreen(MainScreen main)
    {
        _main = main;

        _main.postResultChannel.OnRaiseEvent.AddListener(HandleResponse);
    }

    public void Initialize(VisualElement root)
    {
        _root = root;
        _emailField = root.Q<TextField>("email-field");
        _passwordField = root.Q<TextField>("password-field");
        _root.Q<Button>("login-btn").clicked += HandleLoginClick;
        _root.Q<Button>("close-btn").clicked += HandleCloseClick;
    }

    private void HandleResponse(NetworkEvent evt)
    {
        var postEvt = evt as PostResultEvent;
        if (postEvt.uri != _path) return;

        if (postEvt.response.type == MsgType.SUCCESS)
        {
            var strEvt = DataEvents.StringDataEvent;
            strEvt.data = postEvt.response.msg;
            _main.tokenChannel.RaiseEvent(strEvt);
        }
    }
    public void Dispose()
    {
        _main.postResultChannel.OnRaiseEvent.RemoveListener(HandleResponse);
    }

    private void HandleCloseClick()
    {
        _main.ClosePopupPanel();
    }

    private void HandleLoginClick()
    {
        var evt = Events.LoginNetworkEvent;
        evt.email = Email;
        evt.password = Password;

        _main.loginChannel.RaiseEvent(evt);
    }


}
