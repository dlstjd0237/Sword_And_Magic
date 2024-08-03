using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class RegisterScreen : IDisposable
{
    private MainScreen _main;
    private VisualElement _root;
    private readonly string _path = "user_register";
    private TextField _emailField, _passwordField, _nameField;

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
    public string Name
    {
        get => _nameField.value;
        set => _nameField.value = value;
    }

    public RegisterScreen( MainScreen main)
    {
     
        _main = main;


        _main.postResultChannel.OnRaiseEvent.AddListener(HandleResponse);
    }
    public void Initialize(VisualElement root)
    {
        _root = root;
        _emailField = root.Q<TextField>("email-field");
        _passwordField = root.Q<TextField>("password-field");
        _nameField = root.Q<TextField>("name-field");

        root.Q<Button>("save-btn").clicked += HandleSaveClick;
        root.Q<Button>("cancel-btn").clicked += HandleCancelClick;
    }
    public void Dispose()
    {
        _main.postResultChannel.OnRaiseEvent.RemoveListener(HandleResponse);
    }
    private void HandleResponse(NetworkEvent evt)
    {
        var postEvt = evt as PostResultEvent;
        if (postEvt.uri != _path) return;

        if (postEvt.response.type == MsgType.SUCCESS)
        {
            Email = string.Empty;
            Password = string.Empty;
            Name = string.Empty;

            _main.ClosePopupPanel();
        }
        Debug.Log(postEvt.response.msg); //이부분도 팝업창을통해 메시지를 출력해야하지만..안할꺼야.
    }

    private void HandleCancelClick()
    {
        _main.ClosePopupPanel();
    }

    private void HandleSaveClick()
    {
        var evt = Events.RegisterNetworkEvent;
        evt.email = Email;
        evt.password = Password;
        evt.name = Name;

        _main.registerChannel.RaiseEvent(evt);

        SceneControlManager.FadeOut(() => SceneManager.LoadScene(1));
    }

 
}
