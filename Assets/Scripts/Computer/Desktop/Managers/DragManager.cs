using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public delegate void OnIconBeginDrag(IconInventorySlot iconInventorySlot);
    public static event OnIconBeginDrag OnBeginDragEvent;
    public delegate void OnIconDrop(PointerEventData eventdata, IconInventorySlot iconInventorySlot);
    public static event OnIconDrop OnDropEvent;
    public IconInventorySlot self;
    public DragUI dragUI;
    public void OnBeginDrag(PointerEventData eventdata)
    {
        OnBeginDragEvent(self);
    }
    public void OnEndDrag(PointerEventData eventdata)
    {
        OnDropEvent(eventdata,self);
    }
}
