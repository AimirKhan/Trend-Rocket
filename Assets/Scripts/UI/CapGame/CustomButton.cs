using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.CapGame
{
    public class CustomButton : Button
    {
        public UnityEvent<PointerEventData> OnButtonDown;
        public UnityEvent<PointerEventData> OnButtonUp;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnButtonDown?.Invoke(eventData);
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnButtonUp?.Invoke(eventData);
        }
    }
}
