using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// TODO: this could be static and just be a singleton.
public class DesktopManager : MonoBehaviour
{
    TextIconInventory Instance;
    List<TextIconInventorySlot> _slots;
    Vector3 _StartPosition;
    void Awake()
    {
        TextIconInventorySlot.OnBeginDragEvent += OnBeginDragEvent;
        TextIconInventorySlot.OnDropEvent += OnDropEvent;
        TextIconInventorySlot.OnDoubleClickEvent += OnDoubleClickEvent;
        TextIconInventorySlot.OnSetSlotEvent += OnSetSlotEvent;
        TextIconInventorySlot.OnRemoveSlotEvent += OnRemoveSlotEvent;
    }
    void Start()
    {
        this.Instance = (TextIconInventory) TextIconInventory.Instance;
        _slots = new List<TextIconInventorySlot>();
        _slots.AddRange(TextIconInventorySlot.FindObjectsOfType<TextIconInventorySlot>());
        _slots.Sort((a, b) => a.index - b.index);
        PopulateInitial();
    }

    /// <summary>
    /// Populates the inventory with some icons.
    /// </summary>
    public void PopulateInitial()
    {
        for (int i = 0; i < this.Instance.InventorySlots.Length; i++) 
        {
            TextIcon instance = null;
            // If an object exists at the specified location.
            if (this.Instance.GetIcon(i, out instance)) {
                Debug.Log($"Found icon at {instance}");
                _slots[i].SetSlot(instance);
            }
        }
    }
    
    /// <summary>
    /// Removes the icon from all the non-specified slots.
    /// </summary>
    public void Clear() 
    {
        for (int i = 0; i < this.Instance.InventorySlots.Length; i++) 
        {
            TextIcon instance = null;
            // If an object exists at the specified location.
            if (this.Instance.GetIcon(i, out instance)) {
                _slots[i].RemoveSlot(instance);
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

    // FIXME: All of this is super slow..
    // WARNING: This drops the FPS by a lot!!!!
    /// <summary>
    /// Opens the inventory.
    /// </summary>
    /// <param name="textIcon">The icon that was clicked.</param>
    public void OnDoubleClickEvent(PointerEventData eventData)
    {
        TextIcon instance = eventData.selectedObject.GetComponent<TextIcon>();
        //WindowJsonEditor, WindowLuaEditor
        string windowType = $"Window{instance.textType.ToString()}Editor";
        Debug.Log(windowType);
        GameObject window = Instantiate(Resources.Load($"Computer/Window/{windowType}") as GameObject, transform.parent);
        
        window.GetComponent<WindowMaker>().CreateWindow(instance);

        window.transform.SetParent(transform.parent);
    }

    void OnBeginDragEvent(PointerEventData eventData){
        _StartPosition = transform.position;
    }

    void OnDropEvent(PointerEventData eventData){
        TextIcon textIcon = null;
        // Debug.Log(_GameObject.transform.position);
        // Make the X and Y position of the icon snap to the center of the slot.
        int x = (10 + Mathf.RoundToInt(eventData.position.x / .4f));
        int y = ( 8 - Mathf.RoundToInt(eventData.position.y / .4f));
        int slotindex = (y * 8) + x;
        Debug.Log($"{eventData.position.x}, {eventData.position.y}\n{x}, {y} = {slotindex}");
        // this.Instance.GetIcon(slotindex, out textIcon);
        // if(this.Instance.InsertIcon(slotindex,textIcon) != -1)
        // {
        //     _slots[slotindex].PhysicalRepresentation.transform.position = new Vector3(x * .4f, y * .4f, 0);
        //     // Remove the icon from the slot.
        //     this.Instance.RemoveIcon(slotindex);
        //     SaveManager<TextIcon>.SaveInventory();
        //     SaveManager<TextIcon>.LoadOrInitializeInventory();

        //     Refresh();
        // }
        // else
        // {
        //     Debug.Log("Failed to insert Icon");
        //     _slots[slotindex].PhysicalRepresentation.transform.position = _StartPosition;
        // }
    }
    void OnRemoveSlotEvent(TextIcon textIcon){
    }
    void OnSetSlotEvent(TextIcon textIcon){
    }
    /// <summary>
    /// Saves the inventory on quit.
    /// </summary>
    void OnApplicationQuit()
    {
        // Save the inventory.
        SaveManager<TextIcon>.SaveInventory();
    }
}
