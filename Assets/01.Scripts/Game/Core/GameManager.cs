using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SetDataEventCahnnelSO gameStartChnner, gameTimeChannel;
    public NetworkEventChannelSO logChannel, clearLogChannel, rankChannel;

    public Player player;
    public Transform startTrm;

    private bool _isGameStart;
    private float _gameTime;


    private void Awake()
    {
        gameStartChnner.OnRaiseEvent.AddListener(HandleGameStart);
    }
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventBusType.Start, HandleStartEvent);
        GameEventBus.Subscribe(GameEventBusType.Stop, HandleStopEvent);
        GameEventBus.Subscribe(GameEventBusType.End, HandleEndEvent);

    }


    private void OnDisable()
    {
        GameEventBus.UnSubscribe(GameEventBusType.Start, HandleStartEvent);
        GameEventBus.UnSubscribe(GameEventBusType.Stop, HandleStopEvent);
        GameEventBus.UnSubscribe(GameEventBusType.End, HandleEndEvent);

    }

    private void HandleStopEvent()
    {
        _isGameStart = false;
    }
    private void HandleEndEvent()
    {
        HandleGameClear();
    }

    private void HandleStartEvent()
    {
        _isGameStart = true;
    }

    private void Start()
    {
    }

    private void RequestRankData()
    {
        var evt = Events.VoidEvent;
        evt.sendType = SendType.GET;
        rankChannel.RaiseEvent(evt);
    }

    private void HandleGameClear()
    {

        //이제 게임 클리어 기록을 보내야한다.  누가 몇초에 클리어 했농?
        ClearLogNetworkEvent evt = Events.ClearLogNetworkEvent;
        evt.time = _gameTime;

        clearLogChannel.RaiseEvent(evt);

        _isGameStart = false;

        RequestRankData();

    }

    private void HandleGameStart(DataEvent evt)
    {
        RequestRankData();
        ResetGame();
    }

    private void ResetGame()
    {
        if (Mathf.Approximately(_gameTime, 0) == false)
        {
            LogNetworkEvent evt = Events.LogNetworkEvent;
            float y = (player.transform.position - startTrm.position).y;
            y = y < 0 ? 0 : y;
            evt.record = y;

            logChannel.RaiseEvent(evt);

        }

        _gameTime = 0;
        player.transform.position = startTrm.position;
        player.gameObject.SetActive(true);
    }


    private void Update()
    {
        if (_isGameStart)
        {
            _gameTime += Time.deltaTime;
            var evt = DataEvents.FloatDataEvent;
            evt.data = _gameTime;
            gameTimeChannel.RaiseEvent(evt);
        }
    }
}
