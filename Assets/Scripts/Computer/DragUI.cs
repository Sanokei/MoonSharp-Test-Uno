using UnityEngine;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IDragHandler{
    // This is such a garbage way of doing this omg...
    [HideInInspector] public Camera _Camera;
    [SerializeField] private GameObject _GameObject;
    [SerializeField] private RectTransform _GameObjectRectTransform;
    [HideInInspector] public Canvas _Canvas;
    [HideInInspector] public RectTransform _CanvasRectTransform;

    /// <summary>
    /// Makes the icon draggable.
    /// </summary>
    public void OnDrag(PointerEventData data)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_Canvas.transform as RectTransform, data.position, _Camera, out pos);
        Vector2 newPos = new Vector2(Mathf.Clamp(pos.x, -(_CanvasRectTransform.rect.width / 2) + (_GameObjectRectTransform.rect.width / 2), (_CanvasRectTransform.rect.width / 2) - (_GameObjectRectTransform.rect.width / 2)), Mathf.Clamp(pos.y, -(_CanvasRectTransform.rect.height / 2) + (_GameObjectRectTransform.rect.height / 2), _CanvasRectTransform.rect.height / 2 - (_GameObjectRectTransform.rect.height / 2)));
        _GameObject.transform.position = Vector3.Lerp(_GameObject.transform.position, _Canvas.transform.TransformPoint(newPos), Time.deltaTime * 60f);
    }
}
