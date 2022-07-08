using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FileSystem;
using TMPro;
using InGameCodeEditor;

namespace CodeSystem
{
    public class Compile : MonoBehaviour,IPointerClickHandler
    {
        [SerializeField] TextMeshProUGUI filename;
        // private List<string> _compiledCode = new List<string>();

        // public List<string> compiledCode
        // {
        //     get
        //     {
        //         return _compiledCode;
        //     }
        //     set
        //     {
        //         string s = "";
        //         int position = filename.text.LastIndexOf('.');
        //         if (position > -1)
        //             s = filename.text.Substring(position + 1);
        //         _compiledCode.Add(value);
        //         _compiledCode.Add(s);
        //     }
        // }

        private string _compiledCode;
        public string compiledCode
        {
            get
            {
                return _compiledCode;
            }
            set
            {
                _compiledCode = value;
            }
        }

        [SerializeField] CodeEditor text;
        public void OnPointerClick(PointerEventData eventData)
        {
            CompileCode();
        }

        public void CompileCode()
        {
            _compiledCode = text.Text;
        }
    }
}