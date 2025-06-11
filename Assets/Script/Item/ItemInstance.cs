using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemInstance
{
    public ItemData itemData;
    public int quantity;
    public float durability;

    public ItemInstance(ItemData itemData, int quantity, float durablility)
    {
        this.itemData = itemData;
        this.quantity = quantity;
        this.durability = durablility;
    }

    public void ChangeQauntity(int quantity)
    {
        this.quantity += quantity;
    }
}
