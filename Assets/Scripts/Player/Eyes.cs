using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Eyes : MonoBehaviour
{
    [SerializeField] private Camera _Camera;
    public delegate void OnRayCastHit(RaycastHit hit);
    public static event OnRayCastHit OnRayCastHitEvent;
    public delegate void OnHackerEyes(RaycastHit hit);
    public static event OnHackerEyes OnHackerEyesEvent;
    public delegate void OnSecurityBox(RaycastHit hit);
    public static event OnSecurityBox OnSecurityBoxEvent;
    public delegate void OnProgrammableObject(RaycastHit hit);
    public static event OnProgrammableObject OnProgrammableObjectEvent;

    void Start()
    {
        // OnSecurityBoxEvent += OnSecurityBoxEventHandler;
        // OnProgrammableObjectEvent += OnProgrammableObjectEventHandler;
    }
    bool _computerMode = false;
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1)){
            RaycastHit hit;
            if(RaycastRequest(out hit))
            {
                if(hit.transform.tag == "Computer")
                {
                    OnComputerModeEvent(hit);
                }
                else if(hit.transform.tag == "SecurityBox")
                {
                    OnSecurityBoxEvent(hit);
                }
                else if(hit.transform.tag == "ProgrammableObject")
                {
                    OnProgrammableObjectEvent(hit);
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            if(!_computerMode)
            {
                OnHackerEyesEvent(new RaycastHit());
            }
        }

        // test if hit.transform.tag == "Programmable Object"
        // put a cover that says "Press H to hack"
    }
    bool RaycastRequest(out RaycastHit hit)
    {
        Ray ray = _Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (OnRayCastHitEvent != null)
            {
                OnRayCastHitEvent(hit);
                return true;
            }
        }
        return false;
    }
    void OnComputerModeEvent(RaycastHit hit)
    {
        if(hit.transform.tag == "Computer")
        {
            Debug.Log(!_computerMode ? "Hacker Mode" : "Normal Mode");
            _computerMode = !_computerMode;

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
