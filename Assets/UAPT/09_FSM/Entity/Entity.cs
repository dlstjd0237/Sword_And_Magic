using UnityEngine;

public class Entity : MonoBehaviour
{
    [HideInInspector] public Animator AnimatorCompo;
    [HideInInspector] public Health HealthCompo;
    public float MaxHelath;

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

        HealthCompo.Initialized(MaxHelath);
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
