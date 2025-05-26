using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PoolObjectManager
{
    [SerializeField] private EnemyPoolObjectManager _enemyPoolObjectManager;
    [SerializeField] private AmmoPoolObjectManager _ammoPoolObjectManager;

    public EnemyPoolObjectManager enemyPoolObjectManager => _enemyPoolObjectManager;
    public AmmoPoolObjectManager ammoPoolObjectManager => _ammoPoolObjectManager;

    public static PoolObjectManager instant;

    private PoolObjectManagerInitDataKit _poolObjectInitKit;

    public void Init(PoolObjectManagerInitDataKit poolObjectInitKit)
    {
        if (instant == null)
        {
            instant = this;
        }
        _poolObjectInitKit = poolObjectInitKit;
        InitPoolObjects();
    }

    public void DeInit()
    {
        if (instant != null)
        {
            instant = null;
        }
    }

    private void InitPoolObjects()
    {
        _enemyPoolObjectManager.InitPools(_poolObjectInitKit.enemies);
        _ammoPoolObjectManager.InitPools(_poolObjectInitKit.ammunitions);
    }
}
