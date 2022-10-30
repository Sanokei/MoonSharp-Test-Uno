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
            API.RunCodeInConsole("run " + filename.text.Split(".")[0], console);
        }
    }
}