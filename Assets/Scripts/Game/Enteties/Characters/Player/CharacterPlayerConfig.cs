using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/GamePlayManager/Enteties/Character/Player", order = 1)]
public class CharacterPlayerConfig : ScriptableObject
{
    [SerializeField] private AmmoTypes _bullet;
    [Tooltip("The horizontal movement speed of the player.")]
    [SerializeField] private float _moveSpeed = 5f;

    [Tooltip("The health of player. It is recommended to keep it on 0")]
    [SerializeField] private float _basicHealth = 0f;
    [SerializeField] private float _basicDamage;
    [SerializeField] private float _basicShootingSpeed;

    [SerializeField] private LayerMask _boosterLayers;

    public AmmoTypes Bullet => _bullet;
    public float MoveSpeed => _moveSpeed;
    public float BasicHealth => _basicHealth;
    public float BasicDamage => _basicDamage;
    public float BasicShootingSpeed => _basicShootingSpeed;
    public LayerMask BoosterLayers => _boosterLayers;
}
