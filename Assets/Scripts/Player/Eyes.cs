using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Eyes : MonoBehaviour
{
    [SerializeField] private Camera _Camera;
    public delegate void OnRayCastHit(RaycastHit hit);
    public static event OnRayCastHit OnRayCastHitEvent;
    public delegate void OnSphereCastHit(RaycastHit hit);
    public static event OnSphereCastHit OnSphereCastHitEvent;
    public delegate void OnNoRayCastHit();
    public static event OnNoRayCastHit OnNoRayCastHitEvent;

    //
    float sphereCastThickness = 1f; // The radius of the cast // sphere cast thicc asf :flushed:
    void FixedUpdate()
    {
        RaycastHit hit;
        if(SendRayCastHit(out hit));
        else
            OnNoRayCastHitEvent?.Invoke();
    }

    bool SendRayCastHit(out RaycastHit hit)
    {
        if(Physics.Raycast(_Camera.ScreenPointToRay(Input.mousePosition), out hit))
        {
            OnRayCastHitEvent?.Invoke(hit);
            return true;
        }
        if (Physics.SphereCast(_Camera.ScreenPointToRay(Input.mousePosition), sphereCastThickness, out hit))
        {
            OnSphereCastHitEvent?.Invoke(hit);
            return true;
        }
        return false;
    }
}



    // private void OnDottedCircleEvent(RaycastHit hit)
    // {
    //     DottedCircleEventData data = new DottedCircleEventData(
    //         EventSystem.current,
    //         hit);

    //     ExecuteEvents.Execute<IDottedCircle>(
    //         hit.transform.gameObject,
    //         data,
    //         DottedCircleEventData.OnLookingNearDelegate);
    // }
