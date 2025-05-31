using System;
using UnityEngine;

public class FireBall : BasicAmmoController
{
    private FireBallConfig _fireBallConfig;

    public override void Init()
    {
        base.Init();

        _fireBallConfig = (FireBallConfig)ammoConfig;
        if (_fireBallConfig == null)
        {
            throw new InvalidCastException($"Expected FireBallConfig but got {ammoConfig.GetType().Name}");
        }

    }

    public override void HitObject(GameObject hitedObject)
    {
        base.HitObject(hitedObject);

        IBurnable burnable = hitedObject.GetComponent<IBurnable>();

        if (burnable != null)
            burnable.SetFire(_fireBallConfig.BasicfireDamage, _fireBallConfig.BasicBurningTime);
    }
}
