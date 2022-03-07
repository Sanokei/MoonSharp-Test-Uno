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
    
    //
    bool _computerMode = false;
    float sphereCastThickness = 1f; // The radius of the cast // sphere cast thicc asf :flushed:
    void Start()
    {
        OnRayCastHitEvent += OnComputerModeEvent;
        OnSphereCastHitEvent += OnComputerModeEvent;
    }
    void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(_Camera.ScreenPointToRay(Input.mousePosition), out hit))
            StartCoroutine(OnRayCastHitEvent?.Invoke(HitType.Ray, hit));
        if (Physics.SphereCast(_Camera.ScreenPointToRay(Input.mousePosition), sphereCastThickness, out hit))
            StartCoroutine(OnSphereCastHitEvent?.Invoke(HitType.Sphere, hit));
        
    }
    IEnumerator OnComputerModeEvent(HitType type, RaycastHit hit)
    {
        // Warning: You have to be looking at the computer to leave it
        // This may cause errors later on
        if(type == HitType.Sphere && hit.transform.tag == "Computer")
        {
            
        }
        if((type == HitType.Ray && (!_computerMode && Input.GetMouseButtonDown(0) && hit.transform.tag == "Computer")) || (_computerMode && Input.GetKeyDown(KeyCode.Escape)))
        {
            Debug.Log(!_computerMode ? "Hacker Mode" : "Normal Mode");
            _computerMode = !_computerMode;

            // Fix to deactivate the input field 
            // https://github.com/Sanokei/Programmed-Dystopia/issues/5
            if(!_computerMode && DeactivateInputFieldEvent != null)
                DeactivateInputFieldEvent();
            
            // Disable the player's movement
            playerMovement.canMove = !_computerMode;
            StartCoroutine(ChangeMouseState(_computerMode)); // I dont remember why I made this a coroutine
        }
        yield return null;
    }
    private IEnumerator ChangeMouseState(bool state)
    {
        yield return new WaitForEndOfFrame();
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }
}
