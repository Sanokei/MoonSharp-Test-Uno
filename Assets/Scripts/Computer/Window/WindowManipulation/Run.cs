using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

namespace CodeSystem
{
    public class Run : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Compile compiler;
        Script script = new Script();

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log(script.DoString(compiler.compiledCode));
        }
    }
}