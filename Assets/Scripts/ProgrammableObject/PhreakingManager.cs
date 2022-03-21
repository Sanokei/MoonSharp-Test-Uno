using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// hehe im so clever 
// preaking hehehe

public class PhreakingManager : MonoBehaviour, IDroppable
{
    [SerializeField] private GameObject _DottedCircle;

    void Awake()
    {
        Eyes.OnSphereCastHitEvent += ShowDottedCircle;
        Eyes.OnNoRayCastHitEvent += HideDottedCircle;
    }
    void Update()
    {
    }
    public void Drop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void PickUp(RaycastHit hit)
    {
        
    }

    public void ShowDottedCircle(RaycastHit hit)
    {
        if(hit.transform.tag == "Phone")
        {
            _DottedCircle.SetActive(true);
        }
        else
        {
            _DottedCircle.SetActive(false);
        }
    }
    public void HideDottedCircle()
    {
        _DottedCircle.SetActive(false);
    }
}
