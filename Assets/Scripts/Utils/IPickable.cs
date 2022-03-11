using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IPickable : IEventSystemHandler
{
    // Unity messaging system is so fucking useful my god
    // ExecuteEvents.Execute<IPickable>(gameObject, null, (x,y)=>x.Message1());
    void PickUp(PointerEventData eventData);
    void Drop(PointerEventData eventData);
}
