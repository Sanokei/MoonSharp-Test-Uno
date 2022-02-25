using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// TODO: this could be static and just be a singleton.
public class DesktopManager : MonoBehaviour
{
    TextIconInventory Instance;
    List<TextIconInventorySlot> _Slots;
    Vector3 _StartPosition;
    TextIcon _SelectedIcon;
    public Camera _Camera;
    public Canvas _Canvas;
    public RectTransform _CanvasRectTransform;
    void Awake()
    {
        DragUI.OnBeginDragEvent += OnBeginDragEvent;
        TextIconInventorySlot.OnDoubleClickEvent += OnDoubleClickEvent;
        TextIconInventorySlot.OnSetSlotEvent += OnSetSlotEvent;
        TextIconInventorySlot.OnRemoveSlotEvent += OnRemoveSlotEvent;
    }
    void Start()
    {
        this.Instance = (TextIconInventory) TextIconInventory.Instance;
        _Slots = new List<TextIconInventorySlot>();
        _Slots.AddRange(TextIconInventorySlot.FindObjectsOfType<TextIconInventorySlot>());
        _Slots.Sort((a, b) => a.index - b.index);
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
                // Debug.Log($"Found icon at {instance}");
                _Slots[i].SetSlot(instance);
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
                _Slots[i].RemoveSlot(instance);
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
    public void OnDoubleClickEvent(TextIcon instance)
    {
        // get the index of the ico nwith the start position variable then get the icon with
        // inventory instance
        string windowType = $"Window{instance.textType.ToString()}Editor";
        Debug.Log(windowType);
        GameObject window = Instantiate(Resources.Load($"Computer/Window/{windowType}") as GameObject, transform.parent);
        
        window.GetComponent<WindowMaker>().CreateWindow(instance);
        WindowDragUI du = window.GetComponent<WindowDragUI>();
        du._GameObjectRectTransform =  window.GetComponent<RectTransform>();
        du._Canvas = _Canvas;
        du._CanvasRectTransform = _CanvasRectTransform;
        du._Camera = _Camera;
    
        window.transform.SetParent(transform.parent);
    }

    void OnBeginDragEvent(GameObject startingObject){
        if(!startingObject.name.Contains("Icon"))
            return;
        _StartPosition = startingObject.transform.position;
        this.Instance.GetIcon(startingObject.GetComponentInParent<TextIconInventorySlot>().index, out _SelectedIcon);
        DragUI.OnDropEvent += OnDropEvent;
    }

    void OnDropEvent(GameObject draggedObject){
        TextIcon instance;
        this.Instance.GetIcon(draggedObject.GetComponentInParent<TextIconInventorySlot>().index, out instance);
        if(!instance.Equals(_SelectedIcon))
            return;
        // Debug.Log(_GameObject.transform.position);
        // Make the X and Y position of the icon snap to the center of the slot.
        int x = Mathf.Clamp(13 + Mathf.RoundToInt(draggedObject.transform.position.x / 0.3f), 0, 9);
        int y = Mathf.Clamp(10 - Mathf.RoundToInt(draggedObject.transform.position.y / 0.3f), 0, 4);

        int slotindex = ((int)y * 10) + x;
        // Debug.Log($"{draggedObject.transform.position.x}, {draggedObject.transform.position.y}\n{x}, {y} = {slotindex}");
        TextIcon textIcon = null;
        this.Instance.GetIcon(slotindex, out textIcon);
        if(this.Instance.InsertIcon(slotindex,textIcon) != -1)
        {
            _Slots[slotindex].PhysicalRepresentation.transform.position = new Vector3(x * .3f, y * .4f, 0);
            // Remove the icon from the slot.
            this.Instance.RemoveIcon(slotindex);
            SaveManager<TextIcon>.SaveInventory();
            SaveManager<TextIcon>.LoadOrInitializeInventory();

            Refresh();
        }
        else
        {
            Debug.Log("Failed to insert Icon");
            _Slots[slotindex].PhysicalRepresentation.transform.position = _StartPosition;
        }
        
        DragUI.OnDropEvent -= OnDropEvent;
    }
    void OnRemoveSlotEvent(TextIcon instance){
    }
    void OnSetSlotEvent(TextIcon instance){
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
