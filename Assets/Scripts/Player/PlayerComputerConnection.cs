using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComputerConnection : MonoBehaviour
{
    public delegate void SpawnedComputerEvent();
    public static event SpawnedComputerEvent OnSpawnedComputerEvent;

    public PlayerMovement playerMovement;
    
    // WARNING: Flag variable. Bad practice.
    bool _computerMode = false;
    bool _computerSpawned = false;

    GameObject Computer;

    void Awake()
    {
        Eyes.OnRayCastHitEvent += OnComputerModeEvent;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(!_computerSpawned)
            {
                // Get the position infront of the player
                Vector3 position = playerMovement.transform.position + playerMovement.transform.forward * 1.05f;
                // Spawn the computer when pressing 1 key
                Computer = (GameObject) Instantiate(Resources.Load("Prefabs/Computer/Computer"), position, Quaternion.identity);
                _computerSpawned = true;
            }
            else
            {
                Destroy(Computer);
                _computerSpawned = false;
            }
        }
    }

    private void OnComputerModeEvent(RaycastHit hit)
    {
        // Fixed.
            // Warning: You have to be looking at the computer to leave it
            // This may cause errors later on.
        if( // yes I know this is gross but its for future readability.
            // if you not in computer mode and you are looking at the computer and click mouse 0 (Left click)
            (!_computerMode && Input.GetMouseButtonDown(0) && hit.transform.tag == "Computer")
            || // or
            // if you are in computer mode and you press escape
            (_computerMode && Input.GetKeyDown(KeyCode.Escape))
            )
        {
            _computerMode = !_computerMode;

            // Disable the player's movement
            playerMovement.canMove = !_computerMode;
            StartCoroutine(Co_ChangeMouseState(_computerMode)); // I dont remember why I made this a coroutine
        }
    }
    private IEnumerator Co_ChangeMouseState(bool state)
    {
        yield return new WaitForEndOfFrame();
        Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = state;
    }
}