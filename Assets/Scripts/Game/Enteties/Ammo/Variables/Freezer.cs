using System;
using UnityEngine;

public class Freezer : BasicAmmoController
{
    private FreezBulletConfig _freezBulletConfig;

    public override void Init()
    {
        base.Init();

        _freezBulletConfig = (FreezBulletConfig)ammoConfig;

        if (_freezBulletConfig == null)
        {
            throw new InvalidCastException($"Expected FreezerConfig but got {ammoConfig.GetType().Name}");
        }
    }

    public override void HitObject(GameObject hitedObject)
    {
        base.HitObject(hitedObject);

        IFreezable freezable = hitedObject.GetComponent<IFreezable>();

        if (freezable != null)
            freezable.Freeze(_freezBulletConfig.BasicFreezTime, _freezBulletConfig.BasicFreezPercent);
    }
}
