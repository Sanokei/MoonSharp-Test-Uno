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
    public DragUI dragUI;
    public void OnBeginDrag(PointerEventData eventdata)
    {
        OnBeginDragEvent(self);
    }
    public void OnEndDrag(PointerEventData eventdata)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(dragUI._Canvas.transform as RectTransform, eventdata.position, dragUI._Camera, out pos);
        // FixMe: this is a bad way to do this.
        int x = Mathf.RoundToInt(((pos.x * 10) + 6)/.3f);
        int y = Mathf.RoundToInt(((pos.y * 10)/.3f) - 1);
        // convert the pos to a slot index
        int slotindex = (int)((y * 10) + x);
        Debug.Log("x: " + x + " y: " + y);
        Debug.Log("x: " + pos.x + " y: " + pos.y + " slotindex: " + slotindex);
        OnDropEvent(0);
    }
}
