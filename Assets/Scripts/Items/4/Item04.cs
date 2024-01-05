using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item04 : BaseItem
{
    public override void OnEverySecond()
    {

    }

    public override void OnGetItem()
    {
        PlayerStatusValues newValues = PlayerStatusHandler.Instance.statusValues;
        newValues.criticalChance += 1f;

        PlayerStatusHandler.Instance.statusValues = newValues;
    }

    public override void OnPlayerCriticalHit(Projectile shoottedBullet, GameObject shootPoint)
    {

    }

    public override void OnPlayerDie()
    {

    }

    public override void OnPlayerKill()
    {

    }

    public override void OnPlayerLevelUp()
    {

    }

    public override void OnPlayerShoot(Projectile shoottedBullet, GameObject shootPoint)
    {

    }

    public override void OnStackUp()
    {
        float newCriticalDamage = PlayerStatusHandler.Instance.statusValues.criticalDamageIncrease;

        newCriticalDamage += 1f;

        PlayerStatusHandler.Instance.statusValues.criticalDamageIncrease = newCriticalDamage;
    }
}
