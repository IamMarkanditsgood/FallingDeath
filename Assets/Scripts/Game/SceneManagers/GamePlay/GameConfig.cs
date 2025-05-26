using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/GamePlayManager/Game/Level", order = 1)]
public class GameConfig : ScriptableObject
{
    [Header("Configs")]
    [SerializeField] private CharacterPlayerConfig _playerConfig;
    [SerializeField] private CameraConfig _cameraConfig;
    [Tooltip("All used configs. You have to add here at list one config of each type of enemy config")]
    [SerializeField] private BasicEnemyConfig[] _enemyConfigs;
    [Tooltip("All used configs. You have to add here at list one config of each type of ammo config")]
    [SerializeField] private BasicAmmoConfig[] _ammoConfigs;

    [Header("Prefab References")]
    [Tooltip("Enemies prefab to spawn")]
    [SerializeField] private BasicEnemyController[] _enemyPrefabs;
    [Tooltip("Ammo prefab to spawn")]
    [SerializeField] private BasicAmmoController[] _ammoPrefabs;
    [Tooltip("Camera prefab reference")]
    [SerializeField] private GameObject _cameraPrefab;
    [Tooltip("Player character prefab")]
    [SerializeField] private GameObject _playerPref;

    [Header("Global game parameters")]
    [Tooltip("Time interval between object garbage collector that clear objects that are to far and not in use. Interval in seconds")]
    [SerializeField] private float _objectGarbageCollectorInterval = 30f;

    [Header("Spawner Settings")]

    [Header("Spawn Timing")]
    [Tooltip("Time interval between platform row spawns (seconds)")]
    [SerializeField] private float _spawnIntervalTimer = 1f;

    [Header("Horizontal Spawn Boundaries")]
    [Tooltip("Enable to set custom X spawn boundaries")]
    [SerializeField] private bool _useManualXBounds = false;
    [Tooltip("Minimum X position when using manual bounds")]
    [SerializeField] private float _manualMinX = -2f;
    [Tooltip("Maximum X position when using manual bounds")]
    [SerializeField] private float _manualMaxX = 2f;


    public CharacterPlayerConfig PlayerConfig => _playerConfig;
    public CameraConfig CameraConfig => _cameraConfig;
    public BasicEnemyConfig[] EnemyConfigs => _enemyConfigs;
    public BasicAmmoConfig[] AmmoConfig => _ammoConfigs;
    public BasicEnemyController[] EnemyPrefabs => _enemyPrefabs;
    public BasicAmmoController[] AmmoPrefabs => _ammoPrefabs;
    public GameObject CameraPrefab => _cameraPrefab;
    public GameObject PlayerPref => _playerPref;
    public float ObjectGarbageCollectorInterval => _objectGarbageCollectorInterval;
    public float SpawnIntervalTimer => _spawnIntervalTimer;
    public bool UseManualXBounds => _useManualXBounds;
    public float ManualMinX => _manualMinX;
    public float ManualMaxX => _manualMaxX;
}
