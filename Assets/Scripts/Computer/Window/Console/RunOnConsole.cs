using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Console;
using Lancet;
using InGameCodeEditor;
using TMPro;

namespace CodeSystem
{
    public class RunOnConsole : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] TMP_Text filename;
        
        ConsoleManager console;

        public void OnPointerClick(PointerEventData eventData)
        {
            if(!console)
                console = ConsoleManager.CreateConsole();
            // Moved all Moonsharp functionality to Assets/Scripts/Lancet
            Debug.Log(console);
            Debug.Log(filename.text);
            console?.OnSubmit("run " + filename.text.Split(".")[0], true);
        }
    }
}