using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/CreateItem", order = 1)]
public class SOItem : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public string itemDescription;
}
