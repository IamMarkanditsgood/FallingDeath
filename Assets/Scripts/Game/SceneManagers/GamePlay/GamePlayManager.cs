using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GamePlayManager
{
    [SerializeField] private float _gameTime = 0f;

    private Camera _mainCamera;
    private GameConfig _gameConfig;
   
    private GameObject _player;

    private Coroutine _gameTimer;

    public bool IsGameStarted { get; private set; }

    public void Init(GameConfig gameConfig, GameObject player)
    {
        _player = player;

        _gameConfig = gameConfig;

        _mainCamera = Camera.main;
    }

    public void DeInit()
    {
        CoroutineServices.instance.StopCoroutine(_gameTimer);
    }

    public void StartGame()
    {
        IsGameStarted = true;
        _gameTimer = CoroutineServices.instance.StartRoutine(GameTimer());
    }


    private IEnumerator GameTimer()
    {
        while (true)
        {
            _gameTime += 1;
            yield return new WaitForSeconds(1);
        }
    }
}