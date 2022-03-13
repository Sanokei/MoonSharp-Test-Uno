using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Eyes : MonoBehaviour
{
    [SerializeField] private Camera _Camera;
    public enum HitType { Ray, Sphere };
    public delegate IEnumerator OnRayCastHit(HitType type, RaycastHit hit);
    public static event OnRayCastHit OnRayCastHitEvent;
    public delegate IEnumerator OnSphereCastHit(HitType type, RaycastHit hit);
    public static event OnSphereCastHit OnSphereCastHitEvent;

    public PlayerMovement playerMovement;

    //
    public delegate void DeactivateInputField();
    public static event DeactivateInputField DeactivateInputFieldEvent;
    
    // FIXME: Flag variable. Bad practice.
    bool _computerMode = false;
    float sphereCastThickness = 1f; // The radius of the cast // sphere cast thicc asf :flushed:
    void Awake()
    {
        OnRayCastHitEvent += Co_OnComputerModeEvent;
        OnSphereCastHitEvent += Co_OnComputerModeEvent;
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(_Camera.ScreenPointToRay(Input.mousePosition), out hit))
            StartCoroutine(OnRayCastHitEvent?.Invoke(HitType.Ray, hit));
        if (Physics.SphereCast(_Camera.ScreenPointToRay(Input.mousePosition), sphereCastThickness, out hit))
            StartCoroutine(OnSphereCastHitEvent?.Invoke(HitType.Sphere, hit));
        
    }
    private IEnumerator Co_OnComputerModeEvent(HitType type, RaycastHit hit)
    {
        // Warning: You have to be looking at the computer to leave it
        // This may cause errors later on
        if(type == HitType.Sphere && !_computerMode && hit.transform.tag == "Computer")
        {
            
        }
        if( // yes I know this is gross but its for future readability.
            (type == HitType.Ray && 
            // if you not in computer mode and you are looking at the computer and click mouse 0 (Left click)
            (!_computerMode && Input.GetMouseButtonDown(0) && hit.transform.tag == "Computer")) 
            || // or 
            // if you are in computer mode and you press escape
            (_computerMode && Input.GetKeyDown(KeyCode.Escape))
            )
        {
            Debug.Log(!_computerMode ? "Hacker Mode" : "Normal Mode");
            _computerMode = !_computerMode;

            // Fix to deactivate the input field 
            // https://github.com/Sanokei/Programmed-Dystopia/issues/5
            if(!_computerMode && DeactivateInputFieldEvent != null)
                DeactivateInputFieldEvent();
            
            // Disable the player's movement
            playerMovement.canMove = !_computerMode;
            StartCoroutine(Co_ChangeMouseState(_computerMode)); // I dont remember why I made this a coroutine
        }
        yield return null;
    }
    private IEnumerator Co_ChangeMouseState(bool state)
    {
        yield return new WaitForEndOfFrame();
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }

    private IEnumerator Co_OnPhonePickUpEvent(HitType type, RaycastHit hit)
    {
        yield return null;
    }
}
