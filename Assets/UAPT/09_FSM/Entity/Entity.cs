using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    private readonly int _attackSpeedHash = Animator.StringToHash("AttackSpeed");

    [HideInInspector] public Animator AnimatorCompo;
    [HideInInspector] public Health HealthCompo;

    private float _maxHelath;
    [SerializeField] public CharactersStat Stat;

    [Header("Collision info")]
    [SerializeField] protected Transform _groundChecker;
    protected Transform _wallChecker;
    [SerializeField] protected float _groundCheckDistance;
    protected float _wallCheckDistance;
    [SerializeField] protected LayerMask _whatIsEnemy;
    [SerializeField] protected LayerMask _whatIsGround;
    [SerializeField] protected LayerMask _whatIsWall;


    protected virtual void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimatorCompo = visualTrm.GetComponent<Animator>();
        HealthCompo = gameObject.GetComponent<Health>();

        Stat = Instantiate(Stat);
        _maxHelath = Stat.maxHealth.GetValue();
        HealthCompo.Initialized(_maxHelath);
    }

    protected virtual void OnEnable()
    {
        Stat.maxHealth.ValueChangeEvent += HandleChangeEvent;
        Stat.attackSpeed.ValueChangeEvent += HandleChangeAttackSpeedEvent;
    }

    protected virtual void OnDisable()
    {
        Stat.maxHealth.ValueChangeEvent -= HandleChangeEvent;
        Stat.attackSpeed.ValueChangeEvent -= HandleChangeAttackSpeedEvent;
    }
    private void HandleChangeAttackSpeedEvent(int value)
    {
        Debug.Log((float)value / 1000);
        AnimatorCompo.SetFloat(_attackSpeedHash, (float)value / 1000);
    }

    private void HandleChangeEvent(int value)
    {
        HealthCompo.SetCurrentHelath(value);
    }

    public virtual bool IsGroundDetected()
    {
        return Physics.Raycast(_groundChecker.position, Vector2.down, _groundCheckDistance, _whatIsGround);
    }

    public virtual bool IsRightWallDetected()
    {
        return Physics.Raycast(_wallChecker.position, _wallChecker.right, _wallCheckDistance, _whatIsWall);
    }

    public virtual bool IsLeftWallDetected()
    {
        return Physics.Raycast(_wallChecker.position, -_wallChecker.right, _wallCheckDistance, _whatIsWall);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_groundChecker.position, Vector2.down * _groundCheckDistance);
    }

}
