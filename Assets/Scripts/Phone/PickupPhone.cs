using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickupPhone : MonoBehaviour
{
    bool isPickedUp = false;
    [SerializeField] GameObject _phone;
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] Rigidbody _phoneRb;

    void Awake()
    {
        Eyes.OnRayCastHitEvent += OnPickUpPhone;
        _phoneRb = _phone.gameObject.GetComponent<Rigidbody>();
    }

    void OnPickUpPhone(RaycastHit hit)
    {
        if(hit.transform.tag == "Phone" && Input.GetKeyDown(KeyCode.E))
        {
            if(!isPickedUp)
            {
                _phone.transform.parent = _playerMovement.gameObject.transform;
                _phone.transform.localPosition = new Vector3(0,0,0) + (_playerMovement.gameObject.transform.forward * 2);
                _phone.transform.rotation = _playerMovement.gameObject.transform.rotation;
                _phoneRb.isKinematic = true;
                isPickedUp = true;
            }
            else
            {
                _phone.transform.parent = null;
                _phoneRb.isKinematic = false;
                isPickedUp = false;
            }
        }
    }

}
