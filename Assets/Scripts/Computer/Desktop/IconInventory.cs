using UnityEngine;

[CreateAssetMenu(menuName = "Icon/Inventory", fileName = "Inventory.asset")]
[System.Serializable]
public class IconInventory : Inventory<Icon> {
    public static IconInventory Instance {
        get {
            Inventory<Icon>[] tmp = Resources.FindObjectsOfTypeAll<Inventory<Icon>>();
            foreach (Inventory<Icon> ins in tmp) {
                if(ins.useAsDefault) {
                    Debug.Log("Found inventory as: " + ins);
                    ins.hideFlags = HideFlags.HideAndDontSave;
                    instance = ins;
                    return (IconInventory) ins;
                }
            }
            Debug.Log("Did not find inventory, loading from file or template.");
            return SaveManager.LoadOrInitializeInventory();
        }
    }
}
