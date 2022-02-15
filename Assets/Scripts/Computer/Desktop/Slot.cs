using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int index = 0;
    private TextIcon iconInstance;    // Inventory backend representation.
    private GameObject prefabInstance = null;    // Inventory frontend representation.
    
    /// <summary> 
    /// Sets the slot to the specified icon.
    /// </summary>
    /// <param name="icon">The icon to set the slot to.</param>
    public void SetSlot(TextIcon instance) 
    {
        this.iconInstance = TextIcon.CreateInstance<TextIcon>(); // Not used... too bad.
        
        // TODO: it would be better if i used SetActive() etc rather than Instantiate/Destroy.
        this.prefabInstance = Instantiate(
            instance.physicalRepresentation,
            transform // Change the transform of the slot to change the transform of the icon.
            )
            as GameObject
        ;

        // All of this wouldnt need to be done if i just used SetActive instead of instantiating new prefabs everytime...
        // {
            // probably should've picked a better name for this lol xd
            this.prefabInstance.GetComponent<IconManager>().text.text = $"{instance.name}.{instance.textType.ToString()}";
            this.prefabInstance.GetComponentInChildren<IconManager>().image.sprite = instance.image;

            // ew.
            this.prefabInstance.GetComponent<DragUI>()._Camera = Camera.main; // TODO: this is a hack.
            this.prefabInstance.GetComponent<DragUI>()._Canvas = GameObject.Find("Screen Canvas").GetComponent<Canvas>();
            this.prefabInstance.GetComponent<DragUI>()._CanvasRectTransform = this.prefabInstance.GetComponent<DragUI>()._Canvas.GetComponent<RectTransform>();

            //
            this.prefabInstance.GetComponent<IconManager>()._TextIcon = instance;
            this.prefabInstance.GetComponent<IconManager>()._Index = index;
            this.prefabInstance.GetComponent<IconManager>()._DesktopManager = this.GetComponentInParent<DesktopManager>();
        // }
        Debug.Log("Slot " + index + " set to " + instance.name);
    }

    // Remove the icon from the slot, and destroy the associated gameobject.
    public void RemoveSlot() 
    {
        this.iconInstance = null;
        Destroy(this.prefabInstance);
        this.prefabInstance = null;
    }
}
