using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WindowDragUI : MonoBehaviour, IDragHandler
{
    
    public Camera _Camera;
    public RectTransform _GameObjectRectTransform;
    public Canvas _Canvas;
    public RectTransform _CanvasRectTransform;
   
    /// <summary>
    /// Makes the icon draggable.
    /// </summary>
    public void OnDrag(PointerEventData eventdata)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_Canvas.transform as RectTransform, eventdata.position, _Camera, out pos);
        Vector2 newPos = new Vector2(Mathf.Clamp(pos.x, -(_CanvasRectTransform.rect.width / 2) + (_GameObjectRectTransform.rect.width / 2), (_CanvasRectTransform.rect.width / 2) - (_GameObjectRectTransform.rect.width / 2)), Mathf.Clamp(pos.y, -(_CanvasRectTransform.rect.height / 2) + (_GameObjectRectTransform.rect.height / 2), _CanvasRectTransform.rect.height / 2 - (_GameObjectRectTransform.rect.height / 2)));
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, _Canvas.transform.TransformPoint(newPos), Time.deltaTime * 60f);
    }
}
