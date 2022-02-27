using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public abstract class IconManager<T> : MonoBehaviour
{
    public delegate void  OnSetSlot(IIcon icon);
    public delegate void OnRemoveSlot(IIcon icon);
    public delegate void OnDoubleClick(IIcon icon);
    // OnDrop only tests if the mouse is over the canvas.
    // public void OnDrop(PointerEventData data)
    // {
    //     OnDropEvent(data);
    // }

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
