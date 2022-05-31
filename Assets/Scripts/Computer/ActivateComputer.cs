using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateComputer : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;
    [SerializeField] GameObject _computer;
    [SerializeField] GameObject _phone;
    Animation _animation;

    private void Start()
    {
        _animation = _phone.GetComponent<Animation>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(_computer.activeSelf)
            {
                _animation.Play("Toaster Hide");
                _computer.SetActive(false);
            }
            else // doesnt work since its disabled lmao
            {
                _computer.SetActive(true);
                _animation.Play("Toaster Pop");
                _computer.transform.position = _playerMovement.gameObject.transform.position + new Vector3(0, 0.1f, 0) + (_playerMovement.gameObject.transform.forward * 2);
                _computer.transform.rotation = _playerMovement.gameObject.transform.rotation;
            }
        }
    }
}
