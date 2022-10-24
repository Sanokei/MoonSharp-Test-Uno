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
        void Start()
        {
            Script.DefaultOptions.DebugPrint = (x) => Debug.Log(x);

            // https://www.moonsharp.org/scriptloaders.html
            ((ScriptLoaderBase)Script.DefaultOptions.ScriptLoader).IgnoreLuaPathGlobal = true;
            ((ScriptLoaderBase)Script.DefaultOptions.ScriptLoader).ModulePaths = ScriptLoaderBase.UnpackStringPaths(System.IO.Path.Combine(Application.persistentDataPath,"?") + "?.lua");
            
            Script.DefaultOptions.ScriptLoader = new LancetScriptLoader();

            // I misundershood how this works
            
            // string folder = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName) + @"\Lancet\";
            // string filter = "*.lua";
            // try{
            //     foreach(var elm in System.IO.Directory.GetFiles(folder, filter))
            //     {
            //         try
            //         {
            //             ((ScriptLoaderBase)Script.DefaultOptions.ScriptLoader).ModulePaths = ScriptLoaderBase.UnpackStringPaths(elm);
            //             Debug.Log($"Loaded module {elm}");
            //         }
            //         catch (System.Exception ex)
            //         {
            //             Debug.Log($"Error loading module{elm} \n{ex}");
            //         }
            //     }
            // }
            // catch(System.Exception ex)
            // {
            //     Debug.Log($"No modules loaded\n{ex}");
            // }
        }

        
        [System.Obsolete("Use RunCodeInConsole instead")]
        public static void RunCode(string code)
        {
            Script script = new Script();
            DynValue fn = script.LoadString(code);
            fn.Function.Call();
        }

        public static void RunCodeInConsole(string command, Dictionary<string,string[]> code, ConsoleManager console, Inventory ConsoleCommands)
        {
            Script script = new Script();
            TextIcon icon = new TextIcon();

            script.Options.DebugPrint = (x) => console.CreateResponse(x);
            
            // FIXME: awful way of getting an icon, but it works for now
            bool _IconExistsFlag = false;
            for(int i = 0; i < ConsoleCommands.GetLength();i++)
            {
                do{
                    ConsoleCommands.GetIcon(i, out icon);
                    ++i;
                }while(i < ConsoleCommands.GetLength() && (icon.textType.ToString().ToLower() != "lua" || !icon.name.ToLower().Contains("_module")));// && icon.name.Split("_module.lua").Length < 1);
                try
                {
                    string name = icon.name.ToLower().Split("_module")[0];
                    _IconExistsFlag = command.ToLower() == name;
                    break;
                }catch(System.Exception ex){}
            }
            if(!_IconExistsFlag)
            {   
                return;
            }
            string[] param;
            try
            {
                param = code[icon.name.ToLower().Split("_module")[0]]; 
                Table arrayValues = new Table(script);
                for(int i=0; i < param.Length; i++)
                {
                    arrayValues.Append(DynValue.NewString(param[i]));
                }
                Debug.Log("Array Values:\n"+arrayValues);

                try
                {
                    DynValue fn = script.LoadString(icon.FileData, arrayValues);
                    fn.Function.Call();
                }
                catch(System.Exception ex)
                {
                    Debug.Log(ex);
                    DynValue fn = script.LoadString($"print('{ex.ToString()}')");
                    fn.Function.Call();
                }
            }
            catch(System.Exception ex)
            {
                Debug.Log(ex);
                DynValue fn = script.LoadString($"print('{ex.ToString()}')");
                fn.Function.Call();
            }
        }
    }
}