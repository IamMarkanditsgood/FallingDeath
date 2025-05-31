using UnityEngine;


[CreateAssetMenu(fileName = "FireBallConfig", menuName = "ScriptableObjects/GamePlayManager/Enteties/Ammo/FireBallConfig", order = 1)]
public class FireBallConfig : BasicAmmoConfig
{
    [SerializeField] private float _basicBurningTime;
    [SerializeField] private float _basicfireDamage;

    public float BasicBurningTime => _basicBurningTime;
    public float BasicfireDamage => _basicfireDamage;
}
