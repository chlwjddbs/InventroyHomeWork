using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipUI : MonoBehaviour
{
    private Player player;
    private Equipment equipment;

    public EquipSlot equipSlotPrefab;
    public Transform equipSlotPosition;
    public List<EquipSlot> equipSlots = new List<EquipSlot>();

    private bool isOpen;
    private EquipSlot selectSlot;

    public void Init(Player player)
    {
        this.player = player;
        equipment = player.equipment;
        player.playerInput.actions["Inventory"].started += Toggle;
        equipment.OnEquipUpdate += OnUpdateSlot;

        for (int i = 0; i < (int)EquipType.totalData; i++)
        {
            equipSlots.Add(Instantiate(equipSlotPrefab, equipSlotPosition));
            equipSlots[i].Init(i);
            equipSlots[i].UpdateSlot(equipment.equipItems[i]);
            equipSlots[i].OnSelectSlotHandler += OnSelectSlot;
            equipSlots[i].OnDeselectSlotHandler += OnDeselectSlot;
            equipSlots[i].OnUnequipSlotHandler += OnUnequip;
        }
        CloseUI();
    }

    public void Toggle(InputAction.CallbackContext context)
    {
        if (isOpen)
        {
            CloseUI();
        }
        else
        {
            OpenUI();
        }
    }

    public void OpenUI()
    {
        isOpen = true;
        Cursor.lockState = CursorLockMode.None;
        gameObject.SetActive(true);
    }

    public void CloseUI()
    {
        isOpen = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);

        selectSlot?.DeselectSlot();
        selectSlot = null;
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    public void OnUpdateSlot(int slot, ItemInstance slotItem)
    {
        equipSlots[slot].UpdateSlot(slotItem);
    }

    public void OnSelectSlot(EquipSlot selectSlot)
    {
        if (this.selectSlot == selectSlot)
        {
            OnDeselectSlot(selectSlot);
            return;
        }

        this.selectSlot?.DeselectSlot();
        this.selectSlot = selectSlot;
        equipment.SelectItem(selectSlot);
    }

    public void OnDeselectSlot(EquipSlot deselectSlot)
    {
        this.selectSlot = null;
        equipment.DeselectItem();
    }

    public void OnUnequip(EquipSlot equipSlot)
    {
        equipment.OnUnequip(equipSlot.slotNum);
    }
}
