using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Game
{
    public class KickDragScreen : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public UnityAction<Vector2> OnDragBegin;
        public UnityAction<Vector2> OnDragProcess;
        public UnityAction<Vector2> OnDragEnd;

        Vector2 startPoint;

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPoint = eventData.position;
            OnDragBegin?.Invoke(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnDragProcess?.Invoke(startPoint - eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnDragEnd?.Invoke(startPoint - eventData.position);
        }
    }
}
