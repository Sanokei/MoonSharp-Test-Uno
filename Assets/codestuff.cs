using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InGameCodeEditor;

public class codestuff : MonoBehaviour
{
    [SerializeField] CodeEditor window1;
    [SerializeField] CodeEditor window2;
    // Start is called before the first frame update
    void Update()
    {    
        window1.Text = "- door has multiple accounts\n- creating new account autofills password\n- capture that data??";
        window2.Text = "-- hacked into door\nlocal network = phone.network\nlocal door = network.find('door')\nlocal code = door.data('code')\n\n-- print the door's source code\nprint(code)\n\nlocal form = code.globalVariable['form']\nlocal submit = form.globalVariable['submitButton']\n\nlocal event = submit.submit()\nlocal prompt = event.getPrompts()\nif(prompt == savePasswordPrompt)\ndo\n-- Have to unhide cuz otherwise its not there to show\n\tprompt.hidePassword('False')";
    }
}
