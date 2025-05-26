using UnityEngine;

public class BasicAmmoConfig : ScriptableObject
{
    [SerializeField] private AmmoTypes _ammoType;
    [SerializeField] private LayerMask _enemyLayers;
    [Tooltip("Time after what object will be switched off")]
    [SerializeField] private float _lifeTime;

    public AmmoTypes AmmoType => _ammoType;
    public LayerMask EnemyLayers => _enemyLayers;
    public float LifeTime => _lifeTime;
}