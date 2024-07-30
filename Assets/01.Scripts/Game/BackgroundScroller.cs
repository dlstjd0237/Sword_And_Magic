using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [Range(0f, 10.0f)]
    [SerializeField] private float _scrollSpeed = 5.0f;
    [SerializeField] private bool _moveBack;
    [SerializeField] private bool _isStop = true;
    public bool IsStop
    {
        get => _isStop;
        set => _isStop = value;

    }
    private int scrollDir;
    private float _offSet;
    private Material _mat;

    private void Awake()
    {
        _mat = GetComponent<Renderer>().material;
        scrollDir = _moveBack == true ? -1 : 1;
    }

    private void OnEnable()
    {

        GameEventBus.Subscribe(GameEventBusType.Start, Continue);
        GameEventBus.Subscribe(GameEventBusType.Stop, Stop);
    }

    private void OnDisable()
    {
        GameEventBus.UnSubscribe(GameEventBusType.Start, Continue);
        GameEventBus.UnSubscribe(GameEventBusType.Stop, Stop);
    }


    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Q))
        //    GameEventBus.Publish(GameEventBusType.Start);
        //if (Input.GetKeyDown(KeyCode.W))
        //    GameEventBus.Publish(GameEventBusType.Stop);


        if (_isStop) return;

        _offSet += (Time.deltaTime * _scrollSpeed * scrollDir) * 0.1f;
        _mat.SetTextureOffset("_MainTex", new Vector2(_offSet, 0));

    }

    private void Stop()
    {
        _isStop = true;

    }

    private void Continue()
    {
        _isStop = false;
    }
}
