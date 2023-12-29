using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item02 : BaseItem
{

    public override void OnEverySecond()
    {

    }

    public override void OnGetItem()
    {
        PlayerStatusValues newValues = PlayerStatusHandler.Instance.statusValues;
        newValues.attackSpeed += 0.3f;

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
        float newAttackSpeed = PlayerStatusHandler.Instance.statusValues.attackSpeed;

        newAttackSpeed += 0.3f;

        PlayerStatusHandler.Instance.statusValues.attackSpeed = newAttackSpeed;
    }
}
