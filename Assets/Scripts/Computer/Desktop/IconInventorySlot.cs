using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class IconInventorySlot : InventorySlot<Icon>, IPointerClickHandler
{
    public delegate void OnDoubleClick(int index);
    public static event OnDoubleClick OnDoubleClickEvent;
    void Awake()
    {
        DesktopManager.OnRemoveSlotEvent += RemoveSlot;
    }
    public override void SetSlot(Icon icon)
    {
        textObject.text = $"{icon.name}";
        imageObject.sprite = icon.image;
        PhysicalRepresentation.SetActive(true);
    }
    
    public override void RemoveSlot(Icon icon)
    {
        PhysicalRepresentation.SetActive(false);
        imageObject.sprite = null;
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            OnDoubleClickEvent(index);
        }
    }
}
