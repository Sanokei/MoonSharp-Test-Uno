using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextIconInventorySlot : InventorySlot<TextIcon>
{
    public static event OnSetSlot OnSetSlotEvent;
    public static event OnRemoveSlot OnRemoveSlotEvent;
    public override void SetSlot(TextIcon icon)
    {
        PhysicalRepresentation.SetActive(true);
        textObject.text = $"{icon.name}.{icon.textType.ToString()}";
        imageObject.sprite = icon.image;
        OnSetSlotEvent(icon);
    }
    
    public override void RemoveSlot(TextIcon icon)
    {
        PhysicalRepresentation.SetActive(false);
        OnRemoveSlotEvent(icon);
    }
}
