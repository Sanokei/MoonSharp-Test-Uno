using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

class HackerEyes : MonoBehaviour
{
    void Start()
    {
        Eyes.OnHackerEyesEvent += OnHackEvent;
    }

    void OnHackEvent(RaycastHit hit)
    {
        // Create lines from programmable object to the security box

        // Create list of Objects nearest to the player that internally are connected
        // to the security box


    }
}
