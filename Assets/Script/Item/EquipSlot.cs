using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IDropHandler, IEndDragHandler
{
    public int slotNum;
    public ItemInstance slotItem;

    public Image itemImage;

    public Button slotButton;

    private bool isSelect;

    public UnityAction<EquipSlot> OnSelectSlotHandler;
    public UnityAction<EquipSlot> OnDeselectSlotHandler;

    public Sprite emptySprite;

    public UnityAction<EquipSlot> OnUnequipSlotHandler;

    public void Init(int slotNum)
    {
        this.slotNum = slotNum;
        slotButton.onClick.AddListener(Toggle);
        itemImage.sprite = emptySprite;
        slotButton.interactable = false;
        DeselectSlot();
    }

    public void AddItem(ItemInstance addItem)
    {
        slotItem = addItem;
        itemImage.sprite = addItem.itemData.itemImage;
    }

    public void RemoveItem()
    {
        slotItem = null;
        itemImage.sprite = emptySprite;
        slotButton.interactable = false;
        DeselectSlot();
    }

    public void UpdateSlot(ItemInstance item)
    {
        if (item != null && item.itemData != null)
        {
            slotItem = item;
            Debug.Log(item.itemData);
            slotButton.interactable = true;
            itemImage.sprite = item.itemData.itemImage;
        }
        else
        {
            RemoveItem();
        }
    }

    public void Toggle()
    {
        if (isSelect)
        {
            DeselectSlot();
        }
        else
        {
            SelectSlot();
        }
    }

    public void SelectSlot()
    {
        if (slotItem?.itemData == null) return;
        isSelect = true;
        OnSelectSlotHandler?.Invoke(this);
    }

    public void DeselectSlot()
    {
        isSelect = false;
        OnDeselectSlotHandler?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnUnequipSlotHandler?.Invoke(this);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}
