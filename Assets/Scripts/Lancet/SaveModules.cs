using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeralizedJSONSystem;

public class SaveModules : MonoBehaviour
{
    void SaveSO()
    {
        string path;
        foreach(TextIcon file in Resources.LoadAll<TextIcon>("Computer/Icon"))
        {
            path = System.IO.Path.Combine(Application.persistentDataPath,UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,$"{file.name}.{file.textType}");
            System.IO.File.WriteAllText(path, file.FileData);
        }
    }
    void Start()
    {
        SaveSO();
    }

    void OnApplicationQuit()
    {
        SaveSO();
    }
}
