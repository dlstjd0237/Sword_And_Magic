using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(PlayerStateMachine))]
public class Player : Entity
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletSpawnTrm;
    private bool _isGameStart = false;
    public bool IsGameStaet => _isGameStart;
    public float BulletDamage = 10;
    public InputRader Input;
    public PlayerStateMachine StateMachine { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();

        foreach (PlayerStateEnum stateEnum in Enum.GetValues(typeof(PlayerStateEnum)))
        {
            string typeName = stateEnum.ToString();
            try
            {
                Type t = Type.GetType($"Player{typeName}State");
                PlayerState state = Activator.CreateInstance(t, this, StateMachine, typeName) as PlayerState;

                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception ex)
            {
                Debug.LogError($"{typeName} is loading error!");
                Debug.LogError(ex);
            }
        }
    }
    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventBusType.Start, HandleStartEvent);
        GameEventBus.Subscribe(GameEventBusType.Stop, HandleStopEvent);
    }
    private void OnDisable()
    {
        GameEventBus.UnSubscribe(GameEventBusType.Start, HandleStartEvent);
        GameEventBus.UnSubscribe(GameEventBusType.Stop, HandleStopEvent);
    }
    private void Start()
    {
        StateMachine.Initialize(PlayerStateEnum.Idle, this);
    }

    private void HandleStopEvent()
    {
        _isGameStart = false;
        StateMachine.ChangeState(PlayerStateEnum.Idle);
    }

    private void HandleStartEvent()
    {
        _isGameStart = true;
        StateMachine.ChangeState(PlayerStateEnum.Attack);

    }
    public void Dead()
    {
        GameEventBus.Publish(GameEventBusType.End);
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
        if (Keyboard.current.sKey.wasPressedThisFrame)
            GameEventBus.Publish(GameEventBusType.Stop);
    }
    public void AttackAnimationEvent()
    {
        var bullet = PoolManager.SpawnFromPool("Bullet", _bulletSpawnTrm.position).GetComponent<Bullet>();
        bullet.Initialized(BulletDamage);
    }

    public void AnimationFinishTrigger()
    {
        StateMachine.ChangeState(PlayerStateEnum.Idle);
    }



}


