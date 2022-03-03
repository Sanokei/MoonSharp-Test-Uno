using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextIconManager : MonoBehaviour
{
    void Start()
    {
        DesktopManager.OnSetSlotEvent += SetSlot;
        DesktopManager.OnCreateWindowEvent += CreateWindow;
    }

    void SetSlot(int index, Icon icon)
    {
        if(icon is TextIcon textIcon)
        {
            DesktopManager._Slots[index].textObject.text += $".{textIcon.textType.ToString()}";
        }
    }
    // FIXME: All of this is super slow..
    // WARNING: This drops the FPS by a lot!!!!
    /// <summary>
    /// Opens the inventory.
    /// </summary>
    /// <param name="textIcon">The icon that was clicked.</param>
    public void CreateWindow(int index, TextIcon textIcon)
    {
        StartCoroutine((IEnumerator)CreateWindowRoutine(index, textIcon));
    }
    private IEnumerator CreateWindowRoutine(int index, TextIcon textIcon)
    {
        // get the index of the ico nwith the start position variable then get the icon with
        // inventory instance
        string windowType = $"Window{textIcon.textType.ToString()}Editor";

        // TODO: This is a bad way to do this.
        GameObject window = Instantiate(Resources.Load($"Computer/Window/{windowType}") as GameObject, transform.parent);
        window.GetComponent<WindowMaker>().CreateWindow(textIcon);
        DragUI du = DesktopManager._Slots[index].PhysicalRepresentation.gameObject.GetComponent<DragUI>();
        du._GameObjectRectTransform = window.GetComponent<DragUI>()._GameObjectRectTransform;
        window.transform.SetParent(du.transform.parent.transform.parent);
        yield return null;
    }
}
