using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active : MonoBehaviour
{
    void OnButtonPress(GameObject go)
    {
        go.SetActive(!go.activeSelf);
    }
}
