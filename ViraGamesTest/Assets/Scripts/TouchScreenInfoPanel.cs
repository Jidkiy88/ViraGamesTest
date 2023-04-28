using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Scripts
{
    public class TouchScreenInfoPanel : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Vector2 _touchScreenPositionFromCenter;
        private Vector2 _limitTouchScreenPosition;
        private Vector2 _touchPositionScreen;
        private Vector2 _deltaTouchScreen;
        private Vector2 _directionTouchMove;
        private Vector2 _directionTouchMoveInPercent;
        private Vector2 _beginTouchPositionScreen;
        private Vector2 _touchScreenInPercent;
        private Vector2 _endTouchPositionScreen;

        public UnityEvent<Vector2> OnDirectionTouchMove;
        public UnityEvent<Vector2> OnDirectionTouchMoveInPercent;
        public UnityEvent<Vector2> OnTouchInPercent;
        public UnityEvent<Vector2> OnTouchDrag;
        public UnityEvent<Vector2> OnTouchDelta;
        public UnityEvent<Vector2> OnTouchDown;
        public UnityEvent<Vector2> OnTouchUp;
        public UnityEvent<Vector2> OnTouchBeginDrag;


        public void OnBeginDrag(PointerEventData eventData)
        {
            OnTouchBeginDrag?.Invoke(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2Int screenCenter = new Vector2Int(Screen.width, Screen.height) / 2;
            _touchScreenPositionFromCenter.x = eventData.position.x - screenCenter.x;
            _touchScreenPositionFromCenter.y = eventData.position.y - screenCenter.y;

            _touchPositionScreen = eventData.position;
            _deltaTouchScreen = eventData.delta;

            _directionTouchMove = _touchScreenPositionFromCenter - (_beginTouchPositionScreen - screenCenter);
            _directionTouchMoveInPercent = _directionTouchMove / screenCenter;

            OnDirectionTouchMove?.Invoke(_directionTouchMove);
            OnDirectionTouchMoveInPercent?.Invoke(_directionTouchMoveInPercent);
            OnTouchInPercent?.Invoke(_touchScreenInPercent);
            OnTouchDrag?.Invoke(_touchPositionScreen);
            OnTouchDelta?.Invoke(_deltaTouchScreen);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("Down");
            _beginTouchPositionScreen = eventData.position;
            OnTouchDown?.Invoke(_beginTouchPositionScreen);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _endTouchPositionScreen = eventData.position;
            OnTouchUp?.Invoke(_endTouchPositionScreen);
        }
    }
}
