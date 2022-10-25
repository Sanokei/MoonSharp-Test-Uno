using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Lancet;
using System.IO;

namespace Console
{
    public partial class ConsoleManager : MonoBehaviour
    {
        [SerializeField] InputField _CurrentInput;
        [SerializeField] GameObject _CurrentInputModule;
        [SerializeField] VerticalLayoutGroup _vertLayoutGroup;
        [SerializeField] Inventory _ConsoleCommands;
        GameObject _Command;
        GameObject _Response;
        GameObject _Input;
        TextMeshProUGUI _commandText;

        void OnEnable()
        {
            ComputerManager.DeactivateInputFieldEvent += DeactivateInputField;
            _CurrentInput.onSubmit.AddListener(OnSubmit);
        }
        void OnDisable()
        {
            ComputerManager.DeactivateInputFieldEvent -= DeactivateInputField;
        }

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
            _CurrentInput.onSubmit.AddListener(OnSubmit);
        }
        public virtual void OnSubmit(string eventData)
        {
            OnSubmit(eventData, false);
        }
        public virtual void OnSubmit(string eventData, bool OVERRIDE = false)
        {
            if(OVERRIDE)
                Start();
            if(_CurrentInput.isFocused || OVERRIDE)
            {
                // delete the input
                Destroy(_CurrentInputModule);

                // make a command
                if(!OVERRIDE)
                {
                    var newC = Instantiate(_Command, new Vector3(0,0,0), Quaternion.identity);
                    newC.transform.SetParent(_vertLayoutGroup.transform,false);
                    //FixME: GetComponent slow
                    _commandText = newC.GetComponentInChildren<TextMeshProUGUI>();
                    _commandText.text = eventData;
                }

                // make a response
                Lancet.API.RunCodeInConsole(SanatizeInput.Input(eventData), this, _ConsoleCommands);
                
                // remake the input
                var newS = Instantiate(_Input, new Vector3(0,0,0), Quaternion.identity);
                newS.transform.SetParent(_vertLayoutGroup.transform,false);
                SetCurrentInputMod(newS.gameObject,newS.GetComponentInChildren<InputField>());
            }
        }
        public void CreateResponse(string eventData)
        {
            var newR = Instantiate(_Response, new Vector3(0,0,0), Quaternion.identity);
            newR.transform.SetParent(_vertLayoutGroup.transform,false);
            _commandText = newR.GetComponentInChildren<TextMeshProUGUI>();
            _commandText.text = eventData;
        }
        void DeactivateInputField()
        { 
            _CurrentInput.DeactivateInputField();
        }
    }
}