using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consum", menuName = ("Item/ConsumableItem"))]
public class ConsumableItemData : ItemData
{
    public override ItemType itemType => ItemType.consumable;
    public List<ItemEffect> itemEffect;
}

[Serializable]
public class ItemEffect
{
    public ItemEffectType itemEffectType;
    public int Amount;
    public int duration;
}

public enum ItemEffectType
{
    health,
    mana,
    moveSpeed
}

