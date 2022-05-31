using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopManager : InventoryPhysical
{
    // protected override void Awake()
    // {
    //     base.Awake();
    // }

    protected override void OnEnable()
    {
        base.OnEnable();
        iconInventorySlots = new List<IconInventorySlot>();

        // This is a bad way to do this. Slow.
        iconInventorySlots.AddRange(IconInventorySlot.FindObjectsOfType<IconInventorySlot>());
        iconInventorySlots.Sort((a, b) => a.index - b.index);
    }

    protected override void Start()
    {
        base.Start();
    }
}