using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class InGameUI : MonoBehaviour
{
    [SerializeField] private Button _gameStartBtn;

    private void Awake()
    {
        GameEventBus.Subscribe(GameEventBusType.Start, HandleButtonHide);
    }
    private void HandleButtonHide()
    {
        _gameStartBtn.gameObject.SetActive(false);
    }
    private void Start()
    {
        _gameStartBtn.onClick.AddListener(HandleGaemStart);
    }
    private void OnDisable()
    {
        _gameStartBtn.onClick.RemoveListener(HandleGaemStart);
    }
    private void HandleGaemStart()
    {
        Debug.Log("d¤±³Õ¤±³Ä¤À¤Ä¤Ã¾ß");
        GameEventBus.Publish(GameEventBusType.Start);
    }

}
