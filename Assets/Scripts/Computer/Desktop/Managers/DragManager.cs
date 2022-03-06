using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public delegate void OnIconBeginDrag(IconInventorySlot iconInventorySlot);
    public static event OnIconBeginDrag OnBeginDragEvent;
    public delegate void OnIconDrop(int index);
    public static event OnIconDrop OnDropEvent;
    public IconInventorySlot self;
    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent(self);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        int x = Mathf.Clamp(13 + Mathf.RoundToInt(gameObject.transform.position.x / 0.3f), 0, 9);
        int y = Mathf.Clamp(10 - Mathf.RoundToInt(gameObject.transform.position.y / 0.3f), 0, 4);
        int slotindex = ((int)y * 10) + x;

        OnDropEvent(slotindex);
    }
}
