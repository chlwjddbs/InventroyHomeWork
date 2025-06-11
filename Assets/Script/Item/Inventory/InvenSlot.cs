using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IDropHandler , IEndDragHandler
{
    public int slotNum;
    public ItemInstance slotItem;

    public Image itemImage;
    public TextMeshProUGUI quantityText;

    public Button slotButton;

    private bool isSelect;

    public UnityAction<InvenSlot> OnSelectSlotHandler;
    public UnityAction<InvenSlot> OnDeselectSlotHandler;

    public UnityAction<InvenSlot> OnUseSlotHandler;

    public Sprite emptySprite;

    private bool isDrop;

    private Transform dragParent;

    public UnityAction<int, int> OnSwapHandler;
    public UnityAction<InvenSlot> OnDropHandler;


    public void Init(int slotNum)
    {
        this.slotNum = slotNum;
        slotButton.onClick.AddListener(Toggle);
        itemImage.sprite = emptySprite;
        quantityText.enabled = false;
        slotButton.interactable = false;
        dragParent = GameManager.Instance.uiManager.dragParent;
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
        quantityText.enabled = false;
        slotButton.interactable = false;
        DeselectSlot();
    }

    public void UpdateSlot(ItemInstance item)
    {
        if (item != null && item.itemData != null && item.quantity > 0)
        {
            slotItem = item;
            slotButton.interactable = true;
            itemImage.sprite = item.itemData.itemImage;

            if (slotItem.quantity > 1)
            {
                quantityText.enabled = true;
                quantityText.text = slotItem.quantity.ToString();
            }
            else
            {
                quantityText.enabled = false;
            }
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
            OnUseSlotHandler?.Invoke(this);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDrop = false;
        itemImage.transform.SetParent(dragParent);
        itemImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        itemImage.rectTransform.position = eventData.position;
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"End Drop : {slotNum}");

        if (eventData.pointerDrag.TryGetComponent<InvenSlot>(out InvenSlot dropSlot))
        {
            if (this == dropSlot) return;

            ItemInstance tempItem = dropSlot.slotItem;
            dropSlot.slotItem = slotItem;
            dropSlot.UpdateSlot(dropSlot.slotItem);
            dropSlot.isDrop = true;

            slotItem = tempItem;
            UpdateSlot(slotItem);

            OnSwapHandler?.Invoke(slotNum, dropSlot.slotNum);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        itemImage.transform.SetParent(transform);
        itemImage.transform.SetSiblingIndex(1);
        itemImage.transform.localPosition = Vector3.zero;
        itemImage.raycastTarget = true;

        if (!isDrop)
        {
            List<RaycastResult> uiHits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, uiHits);

            if (uiHits.Count == 0)
            {
                OnDropHandler?.Invoke(this);
            }

            return;
        }

        isDrop = false;
    }

  
}
