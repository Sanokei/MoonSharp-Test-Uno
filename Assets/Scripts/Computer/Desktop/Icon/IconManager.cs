using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class IconManager : MonoBehaviour, IDropHandler
{
    public TextMeshProUGUI text;
    public Image image;

    [SerializeField] private GameObject _GameObject;

    public void OnDrop(PointerEventData data)
    {
        // Debug.Log(_GameObject.transform.position);
        // Make the X and Y position of the icon snap to the center of the slot.
        int x = (10 + Mathf.CeilToInt(_GameObject.transform.position.x / .4f));
        int y = ( 8 - Mathf.CeilToInt(_GameObject.transform.position.y / .4f));
        Debug.Log(x + ", " + y);
    }
}
