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

    //
    float sphereCastThickness = 1f; // The radius of the cast // sphere cast thicc asf :flushed:
    void Awake()
    {
        OnSphereCastHitEvent += OnDottedCircleEvent;
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(_Camera.ScreenPointToRay(Input.mousePosition), out hit))
            OnRayCastHitEvent?.Invoke(hit);
        if (Physics.SphereCast(_Camera.ScreenPointToRay(Input.mousePosition), sphereCastThickness, out hit))
            OnSphereCastHitEvent?.Invoke(hit);
    }

    private IEnumerator Co_OnPhonePickUpEvent(RaycastHit hit)
    {
        yield return null;
    }
    private void OnDottedCircleEvent(RaycastHit hit)
    {
        DottedCircleEventData data = new DottedCircleEventData(
            EventSystem.current,
            hit);

        ExecuteEvents.Execute<IDottedCircle>(
            hit.transform.gameObject,
            data,
            DottedCircleEventData.OnLookingNearDelegate);
    }
}
