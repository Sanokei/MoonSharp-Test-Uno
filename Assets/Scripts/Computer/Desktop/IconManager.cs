using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public abstract class IconManager<T> : MonoBehaviour, IDropHandler, IBeginDragHandler{
    public delegate void OnDoubleClick(PointerEventData eventData);
    public delegate void OnIconBeginDrag(PointerEventData eventData);
    public delegate void OnIconDrop(PointerEventData eventData);
    public delegate void  OnSetSlot(T icon);
    public delegate void OnRemoveSlot(T icon);
    // Delegates
    public static event OnIconBeginDrag OnBeginDragEvent;
    public static event OnIconDrop OnDropEvent;

    public void OnBeginDrag(PointerEventData eventData)
    {
        OnBeginDragEvent(eventData);
    }

    public void OnDrop(PointerEventData data)
    {
        OnDropEvent(data);
    }

    /// <summary> 
    /// Sets the slot to the specified icon.
    /// </summary>
    /// <param name="instance">The instance of TextIcon.</param>
    public abstract void SetSlot(T instance);

    // Remove the icon from the slot, and destroy the associated gameobject.
    /// <summary>
    /// Removes the icon from the slot.
    /// </summary>
    public abstract void RemoveSlot(T instance);
}
