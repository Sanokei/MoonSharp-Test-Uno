using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
using MoonSharp;

namespace Lancet
{
    [MoonSharpUserData, MoonSharpHideMember("internal")]
    public class Internal
    {
        public static void run(DynValue code)
        {
            try
            {
                API.RunCodeInConsole(SanatizeInput.Input(code.CastToString()),API.Instance.Current_Console);
            }
            catch(System.Exception ex)
            {
                Debug.Log(ex);
                Dictionary<string,string[]> sanin = SanatizeInput.Input(code.CastToString());
                API.Instance.Current_Console.CreateResponse($"LancetRunTimeError: Filename {API.GetKey(sanin)} may not Exist.");
            }
        }
    }
}