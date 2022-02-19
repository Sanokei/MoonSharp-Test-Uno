using UnityEngine;
using UnityEngine.EventSystems;

// why isnt this a IDraggable interface? idk either...
public class DragUI : MonoBehaviour, IDragHandler{
    // This is such a garbage way of doing this omg...
    public Camera _Camera;
    public RectTransform _GameObjectRectTransform;
    public Canvas _Canvas;
    public RectTransform _CanvasRectTransform;

    /// <summary>
    /// Makes the icon draggable.
    /// </summary>
    public void OnDrag(PointerEventData data)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_Canvas.transform as RectTransform, data.position, _Camera, out pos);
        Vector2 newPos = new Vector2(Mathf.Clamp(pos.x, -(_CanvasRectTransform.rect.width / 2) + (_GameObjectRectTransform.rect.width / 2), (_CanvasRectTransform.rect.width / 2) - (_GameObjectRectTransform.rect.width / 2)), Mathf.Clamp(pos.y, -(_CanvasRectTransform.rect.height / 2) + (_GameObjectRectTransform.rect.height / 2), _CanvasRectTransform.rect.height / 2 - (_GameObjectRectTransform.rect.height / 2)));
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _Canvas.transform.TransformPoint(newPos), Time.deltaTime * 60f);
    }
}
