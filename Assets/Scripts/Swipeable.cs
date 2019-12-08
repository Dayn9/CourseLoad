using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Swipeable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private ScrollRect scrollRect;
    private RectTransform content;

    [Range(0, 1)] private float threshold = 0.75f;

    public UnityEvent OnSwiped;

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        content = (RectTransform)scrollRect.viewport.GetChild(0);
    }

    private void Update()
    {
#if UNITY_EDITOR


#elif UNITY_IOS || UNITY_ANDROID
        if(Input.touchCount > 0)
        {
            Touch t = Input.GetTouch(0);
            scrollRect.enabled = Mathf.Abs(t.deltaPosition.x) > Mathf.Abs(t.deltaPosition.y);
        }
#endif
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        scrollRect.movementType = ScrollRect.MovementType.Unrestricted;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //elastically roll back the 
        scrollRect.movementType = ScrollRect.MovementType.Elastic;

        //check if the content rect has been moved past the the threshold 
        if (Mathf.Abs(content.anchoredPosition.x) > content.rect.width * threshold)
        {
            OnSwiped?.Invoke();
        }
    }
}
