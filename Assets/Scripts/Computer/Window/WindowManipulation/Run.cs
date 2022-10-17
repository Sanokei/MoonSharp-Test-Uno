using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Lancet;

namespace CodeSystem
{
    public class Run : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Compile compiler;

        public void OnPointerClick(PointerEventData eventData)
        {
            // Moved all Moonsharp functionality to Assets/Scripts/Lancet
            compiler.CompileCode();
            Lancet.API.RunCode(compiler.compiledCode);
        }
    }
}