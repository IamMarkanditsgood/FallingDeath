using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class GamePlayManager
{
    [SerializeField] private float _gameTime = 0f;

    private EnemySpawnManager _enemySpawnManager = new EnemySpawnManager();

    private Camera _mainCamera;
    private GameConfig _gameConfig;
   
    private GameObject _player;

    private Coroutine _gameTimer;
    private Coroutine _enemySpawner;

    private float _currentEnemySpawnInterval;

    public bool IsGameStarted { get; private set; }

    public void Init(GameConfig gameConfig, GameObject player)
    {
        _player = player;

        _gameConfig = gameConfig;

        _mainCamera = Camera.main;

        _currentEnemySpawnInterval = gameConfig.SpawnIntervalTimer;

        _enemySpawnManager.Init(_gameConfig, _mainCamera, _player.transform);
    }

    public void DeInit()
    {
        CoroutineServices.instance.StopRoutine(_gameTimer);
        CoroutineServices.instance.StopRoutine(_enemySpawner);
    }

    public void StartGame()
    {
        IsGameStarted = true;

        _gameTimer = CoroutineServices.instance.StartRoutine(GameTimer());
        _enemySpawner = CoroutineServices.instance.StartRoutine(EnemySpawner());
    }

    public void UpdateGamePlay()
    {

    }

    private IEnumerator GameTimer()
    {
        while (true)
        {
            _gameTime += 1;
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator EnemySpawner()
    {
        while (true)
        {
            _enemySpawnManager.SpawnEnemy();
            yield return new WaitForSeconds(_currentEnemySpawnInterval);
        }
    }
}
