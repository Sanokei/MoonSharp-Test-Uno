using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
[System.Serializable]
public class IconInventorySlot : InventorySlot<Icon>
{
    [HideInInspector] public float distanceToDroppedIcon = float.MaxValue;
    [HideInInspector] public float smallerThan_075;
    [HideInInspector] public float smallerThan_106;
    void Awake()
    {
        DragManager.OnEndDraggedEvent += OnEndDrag;
    }

    // Gets called DIRECTLY from Inventory Managers (i.e DesktopManager)
    public void SetSlot(Icon icon)
    {
        textObject.text = $"{icon.name}";
        imageObject.sprite = icon.image;
        PhysicalRepresentation.SetActive(true);
    }
    
    public void RemoveSlot(Icon icon)
    {
        PhysicalRepresentation.transform.localPosition = transform.localPosition;
        PhysicalRepresentation.SetActive(false);
        imageObject.sprite = null;
    }

    public void OnEndDrag(Vector3 position)
    {
        distanceToDroppedIcon = Vector3.Distance(position, transform.localPosition);
        if(distanceToDroppedIcon > 0.106067f)
        {
            distanceToDroppedIcon = float.MaxValue;
        }
    }
}
