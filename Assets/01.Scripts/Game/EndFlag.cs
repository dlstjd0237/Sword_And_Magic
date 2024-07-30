using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndFlag : MonoBehaviour
{
    private bool _flagEnabled;

    public event Action OnGameEnd;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_flagEnabled) return;

        if (collision.CompareTag("Player"))
        {
            Debug.Log("しけ格醤だけい");
            _flagEnabled = true;
            OnGameEnd?.Invoke();
        }
    }
}
