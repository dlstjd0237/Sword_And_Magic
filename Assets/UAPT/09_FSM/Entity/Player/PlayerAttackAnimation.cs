using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackAnimation : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }
    public void AnimationTrigger()
    {
        _player.AttackAnimationEvent();
    }
    public void AnimationFinishTrigger()
    {
        _player.AnimationFinishTrigger();
    }
}
