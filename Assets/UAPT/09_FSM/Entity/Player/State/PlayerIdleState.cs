using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.Input.AttackEvent += HandleAttackEvent;
    }

    private void HandleAttackEvent()
    {
        if (_player.IsGameStaet == false) return;

        _stateMachine.ChangeState(PlayerStateEnum.Attack);
    }

    public override void Exit()
    {
        _player.Input.AttackEvent -= HandleAttackEvent;
        base.Exit();
    }
}
