using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Eyes : MonoBehaviour
{
    bool _computerMode = false;
    void Update()
    {
        if(!_computerMode && Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Hacker Mode");
            _computerMode = !_computerMode;

            // FIXME: getcomponent<> is slow
            // Disable the player's movement
            
            GetComponent<PlayerMovement>().canMove = false;
            StartCoroutine(ChangeMouseState(true));

        }
        else if(_computerMode && Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Normal Mode");
            _computerMode = !_computerMode;

            GetComponent<PlayerMovement>().canMove = true;
            StartCoroutine(ChangeMouseState(false));
        }

    }
    private IEnumerator ChangeMouseState(bool state)
    {
        yield return new WaitForEndOfFrame();
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }
    // void OnGUI()
    // {
    //     if (_computerMode && Event.current.Equals(Event.KeyboardEvent(KeyCode.Escape.ToString())))
    //     {
    //         Debug.Log("Normal Mode");
    //         _computerMode = !_computerMode;

    //         GetComponent<PlayerMovement>().canMove = true;

    //         // Enable the player's movement
    //     }
    // }
}
