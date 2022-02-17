using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Eyes : MonoBehaviour
{
    bool _computerMode = false;
    void Update()
    {
        if(!_computerMode && Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Hacker Mode");
            _computerMode = !_computerMode;
        }
        if(_computerMode)
        {
            
        }
        else
        {

        }
    }

    void OnGUI()
    {
        if (_computerMode && Event.current.Equals(Event.KeyboardEvent(KeyCode.Escape.ToString())))
        {
            Debug.Log("Escape key is pressed.");
            _computerMode = !_computerMode;
        }
    }
}
