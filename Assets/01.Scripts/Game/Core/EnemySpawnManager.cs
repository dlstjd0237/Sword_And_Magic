using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class EnemySpawnManager : MonoSingleton<EnemySpawnManager>
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _spawnTime = 7;
    private int SpawnEnemyCount = 0;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        GameEventBus.Subscribe(GameEventBusType.Start, HandleStartEvent);
        GameEventBus.Subscribe(GameEventBusType.Stop, HandleStopEvent);

    }

    private void HandleStopEvent()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void HandleStartEvent()
    {
        _coroutine = StartCoroutine(nameof(EnemySpawnCoroutine));
    }

    private IEnumerator EnemySpawnCoroutine()
    {
        var Wait = new WaitForSeconds(_spawnTime);

        while (true)
        {
            yield return Wait;
            var enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            SpawnEnemyCount++;
            float enemyHealth = ((SpawnEnemyCount+Random.Range(1,11)) + 30) * 2;
            enemy.Initialized(enemyHealth);
        }
    }





}
