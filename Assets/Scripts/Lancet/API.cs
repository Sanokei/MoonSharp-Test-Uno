using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

using Console;

// In-game compiler
namespace Lancet
{
    public class API : MonoBehaviour
    {
        public static API Instance{get; private set;}
        public ConsoleManager Current_Console;
        [System.Obsolete("Use RunCodeInConsole instead")]
        void Awake()
        {
            Instance = this;
        }
        [System.Obsolete("Use RunCodeInConsole instead")]
        public static void RunCode(string code)
        {
            Script script = new Script();
            DynValue fn = script.LoadString(code);
            fn.Function.Call();
        }

        public static void RunCodeInConsole(Dictionary<string,string[]> code, ConsoleManager console)
        {
            Script script = new Script();
            string command = "";
            foreach(var key in code.Keys)
            {
                command += key;
            }
            TextIcon icon = Resources.Load<TextIcon>("Computer/Icon/"+command);

            script.Options.DebugPrint = (x) => Instance.Current_Console.CreateResponse(x); 
            ((ScriptLoaderBase)script.Options.ScriptLoader).IgnoreLuaPathGlobal = true;
            ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths = ScriptLoaderBase.UnpackStringPaths(System.IO.Path.Combine(Application.persistentDataPath,"?") + "_module.lua");

            Instance.Current_Console = console;

            UserData.RegisterAssembly();
            script.Globals["internal"] = new Internal();
            
            string[] param;
            param = code.GetValueOrDefault(icon.name); 
            Table arrayValues = new Table(script);
            foreach(var x in param)
                arrayValues.Append(DynValue.NewString(x));
            script.Globals["parameters"] = arrayValues;
            try
            {
                Debug.Log(icon.FileData);
                DynValue fn = script.LoadString(icon.FileData);
                fn.Function.Call();
            }
            catch(ScriptRuntimeException ex)
            {
                DynValue fn = script.LoadString($"print(\"{ex.Message}\")");
                fn.Function.Call();
            }
        }

        public static void RunCodeInConsole(Dictionary<string,string[]> code, ConsoleManager console, Inventory inv)
        {
            if (CheckIfInModule(code,inv))
                RunCodeInConsole(code,console);
            else
            {
                Instance.Current_Console = console;
                console.CreateResponse($"LancetRuntimeError: \"{GetKey(code)}\" does not exist in \"{inv.name}\"");
            }
                
        }

        public static void RunCodeInConsole(Dictionary<string,string[]> code)
        {
            if(!Instance.Current_Console)
                RunCodeInConsole(code,Instance.Current_Console);
        }
        
        public static bool CheckIfInModule(Dictionary<string,string[]> code, Inventory commandInventory)
        {
            // FIXME: awful way of getting an icon, but it works for now
            bool _IconExistsFlag = false;
            TextIcon icon;

            for(int i = 0; i < commandInventory.GetLength();i++)
            {
                commandInventory.GetIcon(i, out icon);
                _IconExistsFlag = code.ContainsKey(icon.name);   
            }

            return _IconExistsFlag;
        }

        public static void RunCodeInConsole(string code, ConsoleManager console)
        {
            // Assume run command
            RunCodeInConsole(SanatizeInput.Input("run " + code), console);
        }
        public static string GetKey(Dictionary<string,string[]> code)
        {
            string key = "";
                foreach(var _key in code.Keys)
                    key += _key;
            return key;
        }
    }
}