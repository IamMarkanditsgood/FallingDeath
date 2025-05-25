using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/GamePlayManager/Enteties/Character/Player", order = 1)]
public class CharacterPlayerConfig : ScriptableObject
{

    [Tooltip("The horizontal movement speed of the player.")]
    [SerializeField] private float _moveSpeed = 5f;

    [Tooltip("The health of player. It is recommended to keep it on 0")]
    [SerializeField] private float _basicHealth = 0f;

    [SerializeField] private LayerMask _boosterLayers;

    public float MoveSpeed => _moveSpeed;
    public float BasicHealth => _basicHealth;
    public LayerMask BoosterLayers => _boosterLayers;
}
