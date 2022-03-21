using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IDottedCircle : IEventSystemHandler
{
    void ShowDottedCircle(DottedCircleEventData eventData);
}
