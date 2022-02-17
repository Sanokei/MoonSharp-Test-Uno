using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class IconManager : MonoBehaviour, 
IDropHandler, IBeginDragHandler, IPointerClickHandler
//,IPointerDownHandler
{
    public TextMeshProUGUI text;
    public Image image;
    [SerializeField] private GameObject _GameObject;
    [HideInInspector] public TextIcon _TextIcon;
    [HideInInspector] public int _Index;
    [HideInInspector] public DesktopManager _DesktopManager;
    Vector3 _StartPosition;
    TextIconInventory Instance;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        _StartPosition = transform.position;
    }

    public void OnDrop(PointerEventData data)
    {
        Instance = TextIconInventory.Instance;
        // Debug.Log(_GameObject.transform.position);
        // Make the X and Y position of the icon snap to the center of the slot.
        int x = (10 + Mathf.RoundToInt(_GameObject.transform.position.x / .4f));
        int y = ( 8 - Mathf.RoundToInt(_GameObject.transform.position.y / .4f));
        int index = (y * 8) + x;
        Debug.Log($"Position:({_GameObject.transform.position.x},{_GameObject.transform.position.y})\nPredicted position: ({x},{y})\nSlot: {index}");
        if(Instance.InsertIcon(index,_TextIcon) != -1)
        {
            _GameObject.transform.position = new Vector3(x * .4f, y * .4f, 0);
            // Remove the icon from the slot.
            Instance.RemoveIcon(_Index);
            SaveManager.SaveInventory();
            SaveManager.LoadOrInitializeInventory();

            //This is utterly fucking retarded
            _DesktopManager.Refresh();
        }
        else
        {
            Debug.Log("Failed to insert Icon");
            _GameObject.transform.position = _StartPosition;
        }
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2)
        {
            Debug.Log("Double Click");
            _DesktopManager.OpenTextEditor(_TextIcon);
        }
    }
}
