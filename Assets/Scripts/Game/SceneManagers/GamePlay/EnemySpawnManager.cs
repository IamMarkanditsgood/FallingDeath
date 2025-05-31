using System;
using UnityEngine;

public class EnemySpawnManager
{
    private GameConfig _gameConfig;
    private Camera _mainCamera;
    private Transform _playerTransform;

    public void Init(GameConfig gameConfig, Camera mainCamera, Transform player)
    {
        _gameConfig = gameConfig;
        _mainCamera = mainCamera;
        _playerTransform = player;
    }

    public void SpawnEnemy()
    {
        Vector3 spawnPos = GetRandomSpawnPos();
        BasicEnemyController enemy = GetRandomEnemy();
        enemy.transform.position = spawnPos;
        enemy.Init();
        enemy.Toggle(true);
    }

    private Vector3 GetRandomSpawnPos()
    {
        float spawnX;

        if (_gameConfig.UseManualXBounds)
        {
            spawnX = UnityEngine.Random.Range(_gameConfig.ManualMinX, _gameConfig.ManualMaxX);
        }
        else
        {
            Vector2 camMin = _mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
            Vector2 camMax = _mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
            spawnX = UnityEngine.Random.Range(camMin.x, camMax.x);
        }

        float spawnY = _playerTransform.position.y + _gameConfig.SpawnHeightOffset;

        return new Vector3(spawnX, spawnY, 0f);
    }

    private BasicEnemyController GetRandomEnemy() // Modern metod 
    {
        EnemyTypes randomEnemy = GetRandomEnemyType();
        BasicEnemyController enemy = PoolObjectManager.instant.enemyPoolObjectManager.GetEnemy(randomEnemy);
        return enemy;
    }

    public EnemyTypes GetRandomEnemyType()
    {
        Array values = Enum.GetValues(typeof(EnemyTypes));
        int randomIndex = UnityEngine.Random.Range(0, values.Length); // UnityEngine.Random
        return (EnemyTypes)values.GetValue(randomIndex);
    }
}