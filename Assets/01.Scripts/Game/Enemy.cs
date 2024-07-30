using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D _rigCompo;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxHealth;
    private Health _health;
    private bool _isGameStart = true;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventBusType.Start, HandleStartEvent);
        GameEventBus.Subscribe(GameEventBusType.Stop, HandleStopEvent);
    }

    private void HandleStopEvent()
    {
        _isGameStart = true;
    }

    private void HandleStartEvent()
    {
        _isGameStart = false;

    }

    private void OnDisable()
    {
        GameEventBus.UnSubscribe(GameEventBusType.Start, HandleStartEvent);
        GameEventBus.UnSubscribe(GameEventBusType.Stop, HandleStopEvent);
    }

    public void Initialized(float value)
    {
        maxHealth = value;
        _health.Initialized(maxHealth);
    }
    private void Awake()
    {
        _rigCompo = GetComponent<Rigidbody2D>();
        _health = GetComponent<Health>();

    }
    private void FixedUpdate()
    {
        if (_isGameStart == false)
            return;
        _rigCompo.velocity = new Vector2(-1 * moveSpeed, _rigCompo.velocity.y);
    }

}
