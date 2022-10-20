using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Console;

public class InputPassUpwards : MonoBehaviour
{
    // This is dumb i know
    ConsoleManager cm;
    [SerializeField] InputField _input;
    void OnEnable()
    {
        cm?.GetComponent<ConsoleManager>();
    }

    public void OnSubmit(string s)
    {
        cm.SetCurrentInputMod(gameObject,_input);
        cm.OnSubmit(s);
    }
}
