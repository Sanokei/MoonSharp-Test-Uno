using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerManager : MonoBehaviour
{
    private static ComputerManager instance;
    public static ComputerManager Instance
    {
        get
        {
            if (instance == null)
                instance = new ComputerManager();
            return instance;
        }
    }

    void Update()
    {
    }
}
