using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pickup
{
    public class HandMaker
    {
        public GameObject pickUp {get; private set;}
        public Rigidbody objectRb {get; private set;}
         public HandMaker()
         {
             pickUp = null;
             objectRb = null;
         }
        public HandMaker( GameObject _pickUp, Rigidbody _objectRb )
        {
            this.pickUp = _pickUp;
            this.objectRb = _objectRb;
        }
    }
    public class Hand : MonoBehaviour
    {
        bool _isInHand = false;
        [SerializeField] PlayerMovement _playerMovement;
        void Awake()
        {
            Eyes.OnRayCastHitEvent += OnPickupEvent;
        }
        private HandMaker _inHand = null;
        public HandMaker inHand
        {
            set
            {
                if (value == null)
                    _inHand = null;

                if (_inHand == null)
                    _inHand = value;
            }
            get
            {
                return _inHand; 
            }
        }
        void Update()
        {
            if(_isInHand)
            {
                inHand.pickUp.transform.position = gameObject.transform.position;
                inHand.objectRb.rotation = _playerMovement.gameObject.transform.rotation;
            }
        }
        private void OnPickupEvent(RaycastHit hit)
        {
                if(Input.GetKeyDown(KeyCode.E) && _inHand != null)
                {
                    _isInHand = !_isInHand;
                    inHand.objectRb.useGravity = !_isInHand;
                }
        }
    }
}