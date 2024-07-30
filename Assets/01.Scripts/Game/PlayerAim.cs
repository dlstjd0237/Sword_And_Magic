using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [field: SerializeField] public Transform ArrowTrm { get; private set; }
    //private Player _player;
    //public void Initialize(Player player)
    //{
    //    _player = player;
    //}

    private void Update()
    {
        //Vector3 mousePosition = _player.InputCompo.MousePos;
        //Vector3 direction = mousePosition - transform.position;

        //ArrowTrm.right += direction;
    }
}
