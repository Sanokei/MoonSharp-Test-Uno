using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// hehe im so clever 
// preaking hehehe

public class PhreakingManager : MonoBehaviour, IPickable, IDottedCircle
{
    [SerializeField] private GameObject _DottedCircle;

    void Awake()
    {
        _DottedCircle.SetActive(false);
    }
    public void Drop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void PickUp(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void ShowDottedCircle(DottedCircleEventData eventData)
    {
        if(eventData.hit.transform.tag == "Phone")
        {
            _DottedCircle.SetActive(true);
        }
        else
        {
            _DottedCircle.SetActive(false);
        }
    }
}
