using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equip", menuName = ("Item/EquipItem"))]
public class EquipItemData : ItemData
{
    public override ItemType itemType => ItemType.equip;
    public EquipType equipType;
    public int damage;
    public int defence;
    public int health;
    public int mana;
    public int moveSpeed;

    public GameObject equipModelPrefab;
}

public enum EquipType
{
    weapon,
    armor,
    ring,
    totalData
}