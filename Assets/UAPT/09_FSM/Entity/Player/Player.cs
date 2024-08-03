using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerStateMachine))]
public class Player : Entity
{
    [SerializeField] private string _endScreenName;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _bulletSpawnTrm;
    private bool _isGameStart = false;
    public bool IsGameStaet => _isGameStart;
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

    protected override void OnDisable()
    {
        base.OnDisable();
        GameEventBus.UnSubscribe(GameEventBusType.Start, HandleStartEvent);
        GameEventBus.UnSubscribe(GameEventBusType.Stop, HandleStopEvent);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        GameEventBus.Subscribe(GameEventBusType.Start, HandleStartEvent);
        GameEventBus.Subscribe(GameEventBusType.Stop, HandleStopEvent);
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
        SceneControlManager.FadeOut(() => SceneManager.LoadScene(_endScreenName));
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
        bullet.Initialized(Stat.attackDamage.GetValue());
    }

    public void AnimationFinishTrigger()
    {
        StateMachine.ChangeState(PlayerStateEnum.Idle);
    }



}


