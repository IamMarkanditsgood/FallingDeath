using UnityEngine;

[CreateAssetMenu(fileName = "FreezeBulletConfig", menuName = "ScriptableObjects/GamePlayManager/Enteties/Ammo/FreezeBulletConfig", order = 1)]
public class FreezBulletConfig : BasicAmmoConfig
{
    [SerializeField] private float _basicFreezTime;
    [SerializeField] private float _basicFreezPercent;

    public float BasicFreezTime => _basicFreezTime;
    public float BasicFreezPercent => _basicFreezPercent;
}
