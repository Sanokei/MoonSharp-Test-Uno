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
        void Awake()
        {
            Eyes.OnRayCastHitEvent += OnPickupEvent;
        }
        private HandMaker _inHand;
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

        private void OnPickupEvent(RaycastHit hit)
        {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    inHand.pickUp.transform.position = gameObject.transform.position;
                    inHand.objectRb.useGravity = false;
                }
        }
    }
}