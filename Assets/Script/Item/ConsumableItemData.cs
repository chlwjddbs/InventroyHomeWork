using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consum", menuName = ("Item/ConsumableItem"))]
public class ConsumableItemData : ItemData
{
    public override ItemType itemType => ItemType.consumable;
    public List<ConsumeEffect> consumEffect;
}

[Serializable]
public class ConsumeEffect
{
    public ConsumableType consumableType;
    public int Amount;
    public int duration;
}

public enum ConsumableType
{
    health,
    mana,
    moveSpeed
}

