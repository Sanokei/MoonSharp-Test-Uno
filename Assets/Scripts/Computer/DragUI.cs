using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragUI : MonoBehaviour, IDragHandler{
    // optomize later when instantiating
    // make them public then
    // instatiatedObject._GameObject.SetImage(etc);
    
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _GameObject;
    [SerializeField] private RectTransform _GameObjectRectTransform;
    [SerializeField] private Canvas _Canvas;
    [SerializeField] private RectTransform _canvasRectTransform;
    Vector2 pos;
    
    public void OnDrag(PointerEventData data)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_Canvas.transform as RectTransform, data.position, _camera, out pos);
        Vector2 newPos = new Vector2(Mathf.Clamp(pos.x, -(_canvasRectTransform.rect.width / 2) + (_GameObjectRectTransform.rect.width / 2), (_canvasRectTransform.rect.width / 2) - (_GameObjectRectTransform.rect.width / 2)), Mathf.Clamp(pos.y, -(_canvasRectTransform.rect.height / 2) + (_GameObjectRectTransform.rect.height / 2), _canvasRectTransform.rect.height / 2 - (_GameObjectRectTransform.rect.height / 2)));
        _GameObject.transform.position = Vector3.Lerp(_GameObject.transform.position, _Canvas.transform.TransformPoint(newPos), Time.deltaTime * 60f);
    }
}
