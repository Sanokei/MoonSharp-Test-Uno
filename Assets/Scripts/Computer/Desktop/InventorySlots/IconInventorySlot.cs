using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class IconInventorySlot : InventorySlot<Icon>, IPointerClickHandler
{
    public delegate void OnDoubleClick(IconInventorySlot slot);
    public static event OnDoubleClick OnDoubleClickEvent;
    
    // Gets called DIRECTLY from Inventory Managers (i.e DesktopManager)
    public void SetSlot(Icon icon)
    {
        textObject.text = $"{icon.name}";
        imageObject.sprite = icon.image;
        PhysicalRepresentation.SetActive(true);
    }
    
    public void RemoveSlot(Icon icon)
    {
        PhysicalRepresentation.transform.localPosition = Vector2.zero;
        PhysicalRepresentation.SetActive(false);
        imageObject.sprite = null;
    }
    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            OnDoubleClickEvent(this);
        }
    }
}
