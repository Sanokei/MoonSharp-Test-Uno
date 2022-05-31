using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateComputer : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] GameObject _computer;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(_computer.activeSelf)
            {
                _computer.SetActive(false);
            }
            else // doesnt work since its disabled lmao
            {
                _computer.SetActive(true);
                _computer.transform.position = _playerMovement.gameObject.transform.position + new Vector3(0, 1, 0) + (_playerMovement.gameObject.transform.forward * 2);
                _computer.transform.rotation = _playerMovement.gameObject.transform.rotation;
            }
        }
    }
}
