using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConsoleManager : MonoBehaviour
{
    [SerializeField] InputField _CurrentInput;
    GameObject _Command;
    GameObject _Response;
    GameObject _Input;
    TextMeshProUGUI _commandText;
    // Start is called before the first frame update
    void Start()
    {
        _Command = Resources.Load("Computer/Window/Console/Command") as GameObject;
        _Response = Resources.Load("Computer/Window/Console/Response") as GameObject;
        _Input = Resources.Load("Computer/Window/Console/Input") as GameObject;
    }

    void Update()
    {
    }

    public void OnInputEnter(string eventData)
    {
        Debug.Log(eventData);
        if(_CurrentInput.isFocused)
        {
            // delete the input
            // make a command
            // placement
            // remake the input
            var newC = Instantiate(_Command, new Vector3(0,0,0), Quaternion.identity);
            newC.transform.parent = transform;
            newC.transform.localScale = new Vector3(1,1,1);
            //FixME: GetComponent slow
            _commandText = newC.GetComponentInChildren<TextMeshProUGUI>();
            _commandText.text = eventData;

            Destroy(_CurrentInput);
        }
    }
}

