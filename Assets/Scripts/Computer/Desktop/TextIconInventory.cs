using UnityEngine;

// Change the inventory to something to be able to hold all icons
// TODO: Hold TextIcon and Folders
[CreateAssetMenu(menuName = "Icon/Inventory", fileName = "Inventory.asset")]
[System.Serializable]
public class TextIconInventory : Inventory<TextIcon> {
    public static TextIconInventory Instance {
        get {
            Inventory<TextIcon>[] tmp = Resources.FindObjectsOfTypeAll<Inventory<TextIcon>>();
            foreach (Inventory<TextIcon> ins in tmp) {
                if(ins.useAsDefault) {
                    Debug.Log("Found inventory as: " + ins);
                    ins.hideFlags = HideFlags.HideAndDontSave;
                    instance = ins;
                    return (TextIconInventory) ins;
                }
            }
            Debug.Log("Did not find inventory, loading from file or template.");
            return SaveManager.LoadOrInitializeInventory();
        }
    }
}
