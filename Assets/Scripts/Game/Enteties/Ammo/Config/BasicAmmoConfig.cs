using UnityEngine;

public class BasicAmmoConfig : ScriptableObject
{
    [SerializeField] private AmmoTypes _ammoType;
    [SerializeField] private LayerMask _enemyLayers;
    [Tooltip("Time after what object will be switched off")]
    [SerializeField] private float _lifeTime;
    [SerializeField] private Vector2 _movementDirection;
    [Tooltip("Speed of bullet movement. It should be positive!")]
    [SerializeField] private float _speed;

    public Vector2 MovementDirection => _movementDirection;
    public float Speed => _speed;
    public AmmoTypes AmmoType => _ammoType;
    public LayerMask EnemyLayers => _enemyLayers;
    public float LifeTime => _lifeTime;
}