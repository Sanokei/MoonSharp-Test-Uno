using UnityEngine;
using UnityEngine.EventSystems;

// why isnt this a IDraggable interface? idk either...
public class DragUI : MonoBehaviour, IDragHandler
{
    public Camera _Camera;
    public RectTransform _GameObjectRectTransform;
    public Canvas _Canvas;
    public RectTransform _CanvasRectTransform;

    // public void OnDrop(PointerEventData data)
    // {
    //     OnDropEvent(data);
    // }

    // For some reason the eventdata gives the pointer position
    // which i guess makes sense since it is the eventsystem 
    // that is handling the drag for the mouse not the draggable 
    // gameobject.
    // public void OnEndDrag(PointerEventData eventData)
    // {
    //     OnDropEvent(gameObject);
    // }
    
    /// <summary>
    /// Makes the icon draggable.
    /// </summary>
    public void OnDrag(PointerEventData eventdata)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_Canvas.transform as RectTransform, eventdata.position, _Camera, out pos);

        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        float width = _GameObjectRectTransform.rect.width * _GameObjectRectTransform.localScale.x;
        float height = _GameObjectRectTransform.rect.height * _GameObjectRectTransform.localScale.y;
        // Clamps the position of the dragged object to stay on the screen
        Vector2 newPos = new Vector2(
            Mathf.Clamp(pos.x, -((_CanvasRectTransform.rect.width) / 2) + (width / 2), (_CanvasRectTransform.rect.width / 2) - (width / 2)), 
            Mathf.Clamp(pos.y, -(_CanvasRectTransform.rect.height / 2) + (height / 2), _CanvasRectTransform.rect.height / 2 - (height / 2))
        );
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _Canvas.transform.TransformPoint(newPos), Time.deltaTime * 60f);
    }
}
