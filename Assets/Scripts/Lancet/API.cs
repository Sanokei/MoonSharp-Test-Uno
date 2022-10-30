using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

using Console;

using SeralizedJSONSystem;

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
            // https://www.moonsharp.org/objects.html
            // Automatically register all MoonSharpUserData types
            UserData.RegisterAssembly();
            /*
                // You could also register a class explicitely.
	            UserData.RegisterType<MyClass>();
                // or
	            DynValue obj = UserData.Create(new MyClass());
            
                // meaning you can control which internal commands you want this part of the code to be able to run?
            */
            Script script = new Script();
            script.Options.DebugPrint = (x) => Instance.Current_Console.CreateResponse(x); 
            ((ScriptLoaderBase)script.Options.ScriptLoader).IgnoreLuaPathGlobal = true;
            ((ScriptLoaderBase)script.Options.ScriptLoader).ModulePaths = ScriptLoaderBase.UnpackStringPaths(System.IO.Path.Combine(Application.persistentDataPath,"?") + ".lua");

            string command = "";

            // FIXME: Get rid of this and instead support multiple dictionary 
            // objects instead
            // (for: batch scripts / powershell)
            foreach(var key in code.Keys)
            {
                command += key;
            }

            // Holy fuck im a fucking retarded
            // I was getting the icon from the resources
            // folder which means that the changes made by the user
            // didnt get reflected onto the icon I was getting
            // :skull:
                // TextIcon icon = Resources.Load<TextIcon>("Computer/Icon/"+command);
            TextIcon icon;
            
            // this will output icon as null if file not found
            SeralizedJSON<TextIcon>.LoadScriptableObject(command,out icon);
            // meaning i can check for null without making the nullable
            // TextIcon? icon; // nullable
            // https://stackoverflow.com/questions/58260367/c-sharp-nullable-arguments-assign-to-null-er-use-question-mark
            if(!icon)
            {
                DynValue fn = script.LoadString($"print(\"{command}does not exist\")");
                fn.Function.Call();
                return;
            }
            
            // Assume Icon is got and use 'command' instead of icon.name
            Instance.Current_Console = console;
        
            // adds a lot of the internal commands
            script.Globals["internal"] = new Internal();
            
            string[] param;
            param = code.GetValueOrDefault(command);
            Table arrayValues = new Table(script);
            foreach(var x in param)
                arrayValues.Append(DynValue.NewString(x));
            script.Globals["parameters"] = arrayValues;
            try
            {
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
            RunCodeInConsole(SanatizeInput.Input(code), console);
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