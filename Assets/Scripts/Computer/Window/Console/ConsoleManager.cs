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
        public string[] RemoveFirstFromStringList(int x, string[] str)
        {
            string[] tempStr = new string[str.Length - x];
            for(int i = 0; i < tempStr.Length; i++)
            {
                tempStr[i] = str[i+x];
            }
            return tempStr;
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
                _commandText.text = eventData.ToString();
                
                // make a response

                    // Clean up the eventData String
                    // {command} [parameters] 
                    //  filename  split by spaces
                
                Dictionary<string,string[]> parameters = new Dictionary<string, string[]>();
                string name;
                if(eventData.Contains(" "))
                {
                    parameters.Add(name = eventData.Split(" ")[0],RemoveFirstFromStringList(1,eventData.Split(" ")));
                }
                else
                {
                    parameters.Add(name = eventData,new string[0]);
                }
                Lancet.API.RunCodeInConsole(name, parameters, this, _ConsoleCommands);
                
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