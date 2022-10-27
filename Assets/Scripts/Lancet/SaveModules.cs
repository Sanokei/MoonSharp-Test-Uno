using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SeralizedJSONSystem;

public class SaveModules : MonoBehaviour
{
    void SaveSO()
    {
        foreach(TextIcon file in Resources.LoadAll<TextIcon>("Computer/Icon"))
            SeralizedJSON<TextIcon>.SaveScriptableObject(file,file.name);
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
