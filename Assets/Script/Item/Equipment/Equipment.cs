using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Equipment : MonoBehaviour
{
    public Player player;

    public ItemInstance[] equipItems;

    public Action OnEquipHandler;
    public Action<ItemInstance> OnUnequipHandler;

    public GameObject equipWeaponObj;

    private int selectSlotNum;
    private ItemInstance selectItem;

    public UnityAction<int, ItemInstance> OnEquipUpdate;

    public void Init(Player player)
    {
        this.player = player;
        equipItems = new ItemInstance[(int)EquipType.totalData];

    }

    public bool OnEquip(ItemInstance itemData)
    {
        if(itemData == null) return false ;

        EquipItemData equipItemData = itemData.itemData as EquipItemData ;
        int slotNum = (int)equipItemData.equipType;
        OnUnequip(slotNum);

        switch (equipItemData.equipType)
        {
            case EquipType.weapon:
                equipWeaponObj = Instantiate(equipItemData.equipModelPrefab, player.weaponPos);
                if(equipWeaponObj.TryGetComponent<WeaponHandler>(out WeaponHandler weapon))
                {
                    weapon.Init(player,player.targetMask);
                }
                break;
        }

        equipItems[slotNum] = itemData;
        Debug.Log(itemData.itemData.itemName);
        OnEquipHandler?.Invoke();
        OnEquipUpdate?.Invoke(slotNum,itemData);
        return true;
    }

    public void OnUnequip(int slotNum)
    {
        if (equipItems[slotNum] == null) return;

        if (player.inventory.AddItem(equipItems[slotNum]))
        {
            switch ((equipItems[slotNum].itemData as EquipItemData).equipType)
            {
                case EquipType.weapon:
                    if (equipItems[slotNum] != null)
                    {
                        Destroy(equipWeaponObj);
                    }
                    break;
            }

            OnUnequipHandler?.Invoke(equipItems[slotNum]);
            equipItems[slotNum] = null;
            OnEquipUpdate?.Invoke(slotNum, null);
        }
        else
        {
            Debug.Log("인벤토리 슬롯이 부족합니다. 장착을 해제할 수 없습니다.");
        }
    }

    public void SelectItem(EquipSlot slotData)
    {
        selectSlotNum = slotData.slotNum;
        selectItem = slotData.slotItem;
    }

    public void DeselectItem()
    {
        selectSlotNum = -1;
        selectItem = null;
    }
}
