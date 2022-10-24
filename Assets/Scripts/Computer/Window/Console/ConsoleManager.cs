using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Lancet;

namespace Console
{
    public class ConsoleManager : MonoBehaviour
    {
        [SerializeField] InputField _CurrentInput;
        [SerializeField] GameObject _CurrentInputModule;
        [SerializeField] VerticalLayoutGroup _vertLayoutGroup;
        [SerializeField] Inventory _ConsoleCommands;
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
        public void SetCurrentInputMod(GameObject input, InputField inputField)
        {
            _CurrentInputModule = input;
            _CurrentInput = inputField;
        }

        public virtual void OnSubmit(string eventData)
        {
            if(_CurrentInput.isFocused)
            {
                // delete the input
                Destroy(_CurrentInputModule);

                // make a command
                var newC = Instantiate(_Command, new Vector3(0,0,0), Quaternion.identity);
                newC.transform.SetParent(_vertLayoutGroup.transform,false);
                //FixME: GetComponent slow
                _commandText = newC.GetComponentInChildren<TextMeshProUGUI>();
                _commandText.text = eventData;
                
                // make a response

                    // Clean up the eventData String
                    // {command} [parameters] 
                    //  filename  split by spaces

                Lancet.API.RunCodeInConsole(SanatizeInput.Input(eventData), this, _ConsoleCommands);
                
                // remake the input
                var newS = Instantiate(_Input, new Vector3(0,0,0), Quaternion.identity);
                newS.transform.SetParent(_vertLayoutGroup.transform,false);
            }
        }

        public void CreateResponse(string eventData)
        {
            var newR = Instantiate(_Response, new Vector3(0,0,0), Quaternion.identity);
            newR.transform.SetParent(_vertLayoutGroup.transform,false);
            _commandText = newR.GetComponentInChildren<TextMeshProUGUI>();
            _commandText.text = eventData;
        }
    }
}