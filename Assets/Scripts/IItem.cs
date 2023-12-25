using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IItem{
    public abstract PlayerStatusValues OnGetItem(PlayerStatusValues values);
    public abstract void OnPlayerShoot(GameObject shoottedBullet, GameObject shootPoint);
    public abstract void OnEverySecond();
    public abstract void OnPlayerDie();
    public abstract void OnPlayerKill();
    public abstract void OnPlayerLevelUp();
}
