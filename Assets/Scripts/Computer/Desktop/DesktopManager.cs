using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;

// TODO: this could be static and just be a singleton.
public class DesktopManager : MonoBehaviour
{
    public static Vector3 _StartPosition;
    public static Camera _Camera;
    public static Canvas _Canvas;
    public static RectTransform _CanvasRectTransform;

    // Delegates
    public delegate void  OnSetSlot(int index, Icon icon);
    public static event OnSetSlot OnSetSlotEvent;
    public delegate void OnRemoveSlot(Icon icon);
    public static event OnRemoveSlot OnRemoveSlotEvent;
    public delegate void OnCreateWindow(int index, TextIcon textIcon);
    public static event OnCreateWindow OnCreateWindowEvent;
    public Inventory DesktopInventory;

    // Instance of the ScriptableObject
    public static List<IconInventorySlot> _Slots;

    void Awake()
    {
        DragUI.OnBeginDragEvent += OnBeginDrag;
        IconInventorySlot.OnDoubleClickEvent += DoubleClickEvent;
    }
    void Start()
    {
        DesktopInventory = Inventory.Instance("DesktopInventory");
        _Slots = new List<IconInventorySlot>();
        // this is a bad way to do this.
        _Slots.AddRange(IconInventorySlot.FindObjectsOfType<IconInventorySlot>());
        _Slots.Sort((a, b) => a.index - b.index);
        PopulateInitial();
    }

    /// <summary>
    /// Populates the inventory with some icons.
    /// </summary>
    public void PopulateInitial()
    {
        for (int i = 0; i < DesktopInventory.inventory.Length; i++) 
        {
            Icon instance = null;
            // If an object exists at the specified location.
            if ( DesktopInventory.GetIcon(i, out instance)) {
                // Debug.Log($"Found icon at {instance}");
                _Slots[i].SetSlot(instance);
                OnSetSlotEvent(i, instance);
            }
        }
    }
    
    /// <summary>
    /// Removes the icon from all the non-specified slots.
    /// </summary>
    public void Clear() 
    {
        for (int i = 0; i < DesktopInventory.inventory.Length; i++) 
        {
            Icon instance = null;
            // If an object exists at the specified location.
            if ( DesktopInventory.GetIcon(i, out instance)) {
                _Slots[i].RemoveSlot(instance);
                OnRemoveSlotEvent(instance);
            }
        }
    }

    /// <summary>
    /// Removes the icon from all slots then populates the inventory with the specified icons.
    /// </summary>
    public void Refresh() 
    {
        // TODO: This is a bit of a hack.
        Clear();
        PopulateInitial();
    }

    void OnBeginDrag(GameObject startingObject)
    {
        if(startingObject.tag == "Icon")
        {
            DragUI.OnDropEvent += OnDrop;
            _StartPosition = startingObject.transform.position;
        }
    }
    void OnDrop(GameObject draggedObject)
    {
        if(draggedObject.tag == "Icon")
        {
            // Debug.Log(_GameObject.transform.position);
            // Make the X and Y position of the icon snap to the center of the slot.
            int x = Mathf.Clamp(13 + Mathf.RoundToInt(draggedObject.transform.position.x / 0.3f), 0, 9);
            int y = Mathf.Clamp(10 - Mathf.RoundToInt(draggedObject.transform.position.y / 0.3f), 0, 4);

            int slotindex = ((int)y * 10) + x;

            int oldx = Mathf.Clamp(13 + Mathf.RoundToInt(_StartPosition.x / 0.3f), 0, 9);
            int oldy = Mathf.Clamp(10 - Mathf.RoundToInt(_StartPosition.y / 0.3f), 0, 4);

            int oldSlotIndex = ((int)oldy * 10) + oldx;

            // Debug.Log($"{draggedObject.transform.position.x}, {draggedObject.transform.position.y}\n{x}, {y} = {slotindex}");

            Icon startIcon = null;
            DesktopInventory.GetIcon(oldSlotIndex,out startIcon);
            if(DesktopInventory.InsertIcon(slotindex,startIcon) != -1)
            {
                // Remove the icon from the slot.
                DesktopInventory.RemoveIcon(oldSlotIndex);
                DesktopInventory.SaveInventory("DesktopInventory");
            }
            else
            {
                // Debug.Log("Failed to insert Icon");
                draggedObject.transform.position = _StartPosition;
            }
            Refresh();
            DragUI.OnDropEvent -= OnDrop;
        }
    }

    public void DoubleClickEvent(int index)
    {
        Icon instance;
        DesktopInventory.GetIcon(index, out instance);
        if(instance is TextIcon textIcon)
            OnCreateWindowEvent(index,textIcon);
    }
}
