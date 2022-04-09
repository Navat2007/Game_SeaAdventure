﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
    // Получить основную камеру
    private static Camera _camera;
    public static Camera Camera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    // Кешировать WaitForSeconds
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWait(float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }

    // Узнать был ли щелчок на UI элементе
    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _results;
    public static bool IsOverUi()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
        return _results.Count > 0;
    }

    // Позиция элемента в канвасе
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
        return result;
    }
    
    //Уничтожить все дочерние элементы
    public static void DestroyChild(this Transform transform)
    {
        foreach (Transform child in transform) Object.Destroy(child.gameObject);
    }
}