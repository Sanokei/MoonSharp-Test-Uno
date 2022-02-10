using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace FileSystem
{
    public static class File
    {
        public static bool WriteFile(string filename, string fileData, string fileTag)
        {
            try{
                string path = Application.persistentDataPath + "/ingamefiles/"+filename+"."+fileTag;
                //Write some text to the test.txt file
                StreamWriter writer = new StreamWriter(path, true);
                writer.WriteLine(fileData);
                writer.Close();
                return true;
            }
            catch(System.Exception e){
                Debug.LogError(e.Message);
                return false;
            }
        }
        public static string ReadFile(string filename, string fileTag)
        {
            try{
                string path = Application.persistentDataPath + "/ingamefiles/"+filename+"."+ fileTag;
                //Read the text from directly from the test.txt file
                StreamReader reader = new StreamReader(path);
                return reader.ReadToEnd();
            }
            catch(System.Exception e){
                Debug.LogError(e.Message);
                return "File not found...";
            }
        }
    }
}

