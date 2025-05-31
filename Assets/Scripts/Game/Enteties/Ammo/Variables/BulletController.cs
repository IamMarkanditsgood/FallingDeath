using System;
using UnityEngine;

public class BulletController : BasicAmmoController
{
    private DefaultBulletConfig defaultBulletConfig;

    public override void Init()
    {
        base.Init();

        defaultBulletConfig = ammoConfig as DefaultBulletConfig;

        if (defaultBulletConfig == null)
        {
            throw new InvalidCastException($"Expected DefaultBulletConfig but got {ammoConfig.GetType().Name}");
        }
    }

}
