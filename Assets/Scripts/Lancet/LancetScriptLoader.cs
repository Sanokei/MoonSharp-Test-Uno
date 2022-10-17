using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter.Loaders;
using MoonSharp.Interpreter;

namespace Lancet
{
    public class LancetScriptLoader : ScriptLoaderBase
    {
        public override object LoadFile(string file, Table globalContext)
        {
            Debug.Log($"A request to load '{file}' has been made");
            throw new System.NotImplementedException();
        }
        public override bool ScriptFileExists(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}