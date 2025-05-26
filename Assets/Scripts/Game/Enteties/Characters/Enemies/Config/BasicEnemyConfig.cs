using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/GamePlayManager/Enteties/Character/Enemy", order = 1)]
public class BasicEnemyConfig : ScriptableObject
{
    [SerializeField] private EnemyTypes _enemyType;
    [SerializeField] private bool _canDie;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Vector2 _movementDirection;
    [SerializeField, TagSelector] private string _groundTag;
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private float _basicDamage;    

    public EnemyTypes EnemyType => _enemyType;
    public bool CanDie => _canDie;
    public float MovementSpeed => _movementSpeed;
    public Vector2 MovementDirection => _movementDirection;
    public string GroundTag => _groundTag;
    public LayerMask PlayerLayer => _playerLayer;
    public float BasicDamage => _basicDamage;
}