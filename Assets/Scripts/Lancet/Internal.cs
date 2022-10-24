using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

namespace Lancet
{
    [MoonSharpUserData]
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
                API.Instance.Current_Console.CreateResponse("LancetRunTimeError: Filename unknown and or File may not Exist.");
            }
        }
    }
}