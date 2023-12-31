using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusHandler : MonoBehaviour
{
    public static PlayerStatusHandler Instance;

    public PlayerStatusValues statusValues;

    private void Awake() {
        Instance = this;
    }

}

[Serializable]
public class PlayerStatusValues{
    public int life;
    public float attackSpeed;
    public float damage;
    public float movementSpeed;
    public float baseProjectileSpeed;
    public float criticalChance;
    public float criticalDamageIncrease;
}