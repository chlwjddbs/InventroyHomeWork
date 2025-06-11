using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Player player;
    public InventoryUI InventoryUI;
    public EquipUI EquipUI;

    public Transform dragParent;

    public void Init(Player player)
    {
        InventoryUI.Init(player);
        EquipUI.Init(player);
    }
}
