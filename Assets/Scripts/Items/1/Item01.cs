using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item01 : BaseItem
{
    private float addedMovementSpeed;

    public override void OnEverySecond()
    {

    }

    public override void OnGetItem()
    {
        addedMovementSpeed = 0;
        PlayerStatusValues newValues = PlayerStatusHandler.Instance.statusValues;
        addedMovementSpeed = PlayerStatusHandler.Instance.statusValues.movementSpeed * 0.1f;
        newValues.movementSpeed += addedMovementSpeed;

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
        float newMoveSpeed = PlayerStatusHandler.Instance.statusValues.movementSpeed;

        newMoveSpeed -= addedMovementSpeed;
        addedMovementSpeed = 0;

        float modifier = 0.1f * itemCurrentStack;

        addedMovementSpeed = newMoveSpeed * modifier;
        PlayerStatusHandler.Instance.statusValues.movementSpeed = newMoveSpeed + addedMovementSpeed;

    }

    public override string GetLevelStackDescription()
    {
        return base.GetLevelStackDescription();
    }
}
