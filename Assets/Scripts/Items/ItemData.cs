using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Collectable,
    OneTimePickup,
    AbilityUnlock
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Item/newItem", order = 0)]
public class ItemData : ScriptableObject 
{
    public ItemType Type;
    public string Name;
    public int MaxStack = 1;
    public bool IsStackable = false;
}
