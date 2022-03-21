using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDroppable : IEventSystemHandler
{
    // oh old sano you are so naive, you should be ashamed of yourself
    // unity messaging is so easy, but at the cost of it being so fucking slow a grandmother on a dying pony could
    // probably send the message to china faster than the send message system can send a message to your other classes
        // Unity messaging system is so fucking useful my god
        // ExecuteEvents.Execute<IPickable>(gameObject, null, (x,y)=>x.Message1());
        
    // void PickUp(PointerEventData eventData);
    void Drop(PointerEventData eventData);
}
