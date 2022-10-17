using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ConsoleManager : MonoBehaviour, ISubmitHandler
{
    [SerializeField] InputField _CurrentInput;
    GameObject _Command;
    GameObject _Response;
    GameObject _Input;
    TextMeshProUGUI _commandText;

    public string submittedString; 

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
    public string something(){return "wdad";}
    public void OnSubmit(BaseEventData eventData)
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
            _commandText.text = eventData.ToString();

            Destroy(_CurrentInput);
        }
    }
}

