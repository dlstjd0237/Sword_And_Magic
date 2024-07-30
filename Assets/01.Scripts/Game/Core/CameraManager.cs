using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraManager : MonoSingleton<CameraManager>
{
    [SerializeField] private CinemachineVirtualCamera _titleCam;
    [SerializeField] private CinemachineVirtualCamera _gameCam;


    private void Awake()
    {
        _titleCam.Priority = 0;
        _gameCam.Priority = 0;
    }
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventBusType.Stop, StopEventHandle);
        GameEventBus.Subscribe(GameEventBusType.Start, StartEventHandle);
    }
    private void OnDisable()
    {
        GameEventBus.UnSubscribe(GameEventBusType.Stop, StopEventHandle);
        GameEventBus.UnSubscribe(GameEventBusType.Start, StartEventHandle);
    }

    private void Start()
    {
        GameEventBus.Publish(GameEventBusType.Stop);
    }

    private void StopEventHandle()
    {
        _titleCam.Priority = 10;
        _gameCam.Priority = 0;
    }

    private void StartEventHandle()
    {
        _titleCam.Priority = 0;
        _gameCam.Priority = 10;
    }
}
