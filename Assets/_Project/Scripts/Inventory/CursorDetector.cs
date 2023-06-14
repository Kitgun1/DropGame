using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.Inventory
{
    public class CursorDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Inject] private PlayerCursor.PlayerCursor _cursor;

        public void OnPointerDown(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    _cursor.TryThrowItem(uint.MaxValue);
                    break;
                case PointerEventData.InputButton.Right:
                    _cursor.TryThrowItem(1);
                    break;
                case PointerEventData.InputButton.Middle:
                    _cursor.TryThrowItem(uint.MaxValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    break;
                case PointerEventData.InputButton.Right:
                    break;
                case PointerEventData.InputButton.Middle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}