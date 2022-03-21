using UnityEngine.EventSystems;
using UnityEngine;
// https://gamedev.stackexchange.com/questions/120327/how-to-send-an-interface-message
public class DottedCircleEventData : BaseEventData
{
    // This doesn't have to be in this scope, but I didn't want to create
    // yet another script file for it, so this seemed the most logical place
    public static readonly ExecuteEvents.EventFunction<IDottedCircle> OnLookingNearDelegate =
        delegate (IDottedCircle handler, BaseEventData eventData)
        {
            var casted = ExecuteEvents.ValidateEventData<DottedCircleEventData>(eventData);
            handler.ShowDottedCircle(casted);
        };

    public RaycastHit hit;

    public DottedCircleEventData(EventSystem eventSystem, 
                           RaycastHit hit
                           ) : base(eventSystem)
    {
        this.hit = hit;
    }
}