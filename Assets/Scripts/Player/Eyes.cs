using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Eyes : MonoBehaviour
{
    [SerializeField] private Camera _Camera;
    public delegate void OnRayCastHit(RaycastHit hit);
    public static event OnRayCastHit OnRayCastHitEvent;
    public delegate void DeactivateInputField();
    public static event DeactivateInputField DeactivateInputFieldEvent;
    bool _computerMode = false;
    void Start()
    {
        OnRayCastHitEvent += OnComputerModeEvent;
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        if(RaycastRequest(out hit))
        {
            OnRayCastHitEvent?.Invoke(hit);
        }
    }
    bool RaycastRequest(out RaycastHit hit)
    {
        Ray ray = _Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (OnRayCastHitEvent != null)
            {
                OnRayCastHitEvent(hit);
            }
            return true;
        }
        return false;
    }
    void OnComputerModeEvent(RaycastHit hit)
    {
        // Warning: You have to be looking at the computer to leave it
        // This may cause errors later on
        if((!_computerMode && Input.GetMouseButtonDown(1) && hit.transform.tag == "Computer") || (_computerMode && Input.GetKeyDown(KeyCode.Escape)))
        {
            Debug.Log(!_computerMode ? "Hacker Mode" : "Normal Mode");
            _computerMode = !_computerMode;
            if(!_computerMode && DeactivateInputFieldEvent != null)
                DeactivateInputFieldEvent();
            // FIXME: getcomponent<> is slow
            // Disable the player's movement
            GetComponent<PlayerMovement>().canMove = !_computerMode;
            StartCoroutine(ChangeMouseState(_computerMode));
        }
    }
    private IEnumerator ChangeMouseState(bool state)
    {
        yield return new WaitForEndOfFrame();
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }
}
