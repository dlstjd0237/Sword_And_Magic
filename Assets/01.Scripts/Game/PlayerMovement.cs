using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed, _maxJumpPower, _chargingSpeed;
    private float _currentJumpPower;
    [SerializeField] private Transform _groundCheckerTrm;
    [SerializeField] private Vector2 _checkerSize;
    [SerializeField] private LayerMask _whatIsGround;

    //private Player _player;
    private Rigidbody2D _rigid;

    private Vector2 _inputDirerction;
    private SpriteRenderer _arrowSprite;
    private bool _isCharging = false;

    private bool _isGround;
    public bool IsGround
    {
        get => _isGround;
        set => _isGround = value;
    }

    private bool _canMovement = true;

    //public void Initialize(Player player)
    //{
    //    _player = player;
    //    _rigid = GetComponent<Rigidbody2D>();

    //    _arrowSprite = _player.AimCompo.ArrowTrm.GetComponentInChildren<SpriteRenderer>();

    //    _player.InputCompo.OnJumpEvent += HandleJumpEvent;
    //}

    private void OnDestroy()
    {
        //_player.InputCompo.OnJumpEvent -= HandleJumpEvent;

    }
    private void HandleJumpEvent(bool value)
    {
        if (IsGround == false) return;

        if (value)
        {
            _isCharging = true;
            _canMovement = false;
        }
        else
        {
            _isCharging = false;
            //여기서 점프
            Vector3 dir = _arrowSprite.transform.right;
            _rigid.AddForce(dir * _currentJumpPower, ForceMode2D.Impulse);
            _currentJumpPower = 0;
            _arrowSprite.size = Vector2.one;

            DOVirtual.DelayedCall(0.3f, () => _canMovement = true);
        }
    }

    private void Update()
    {
        //_inputDirerction = _player.InputCompo.Movement;

        if (_isCharging)
        {
            _currentJumpPower += Time.deltaTime * _chargingSpeed;
            _currentJumpPower = Mathf.Clamp(_currentJumpPower, 0, _maxJumpPower);

            _arrowSprite.size = new Vector2(_currentJumpPower / _maxJumpPower * 2.5f + 1, 1);
        }
    }
    private void FixedUpdate()
    {
        CheckIsGround();

        ApplyMovement();
    }

    private void ApplyMovement()
    {
        if (_canMovement == false || _isGround == false) return;

        Vector2 velocity = _rigid.velocity;
        _rigid.velocity = new Vector2(_inputDirerction.x * _moveSpeed, velocity.y);
    }

    private void CheckIsGround()
    {
        IsGround = Physics2D.OverlapBox(_groundCheckerTrm.position, _checkerSize, 0, _whatIsGround);
    }

    private void OnDrawGizmos()
    {
        if (_groundCheckerTrm == null)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_groundCheckerTrm.position, _checkerSize);
        Gizmos.color = Color.white;
    }
}
