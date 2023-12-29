using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseItem : MonoBehaviour
{
    public SOItem itemSO;
    public int itemID;
    public int itemCurrentStack;

    [SerializeField]
    private int itemMaxStack;

    public abstract void OnGetItem();
    public abstract void OnPlayerShoot(Projectile shoottedBullet, GameObject shootPoint);
    public abstract void OnEverySecond();
    public abstract void OnPlayerDie();
    public abstract void OnPlayerKill();
    public abstract void OnPlayerLevelUp();
    public abstract void OnStackUp();
    public abstract void OnPlayerCriticalHit(Projectile shoottedBullet, GameObject shootPoint);

    public SOItem GetItemDetails()
    {
        return itemSO;
    }

    public int GetItemMaxStack()
    {
        return itemMaxStack;
    }

    public virtual string GetLevelStackDescription()
    {
        return "";
    }
}
