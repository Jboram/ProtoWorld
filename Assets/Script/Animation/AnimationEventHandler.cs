using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProtoWorld
{
    public class AnimationEventHandler : MonoBehaviour
    {
        private Dictionary<string, Action> eventHandlers = new Dictionary<string, Action>();
        public void RegisterEvent(string eventName, Action handler)
        {
            if (!eventHandlers.ContainsKey(eventName))
            {
                eventHandlers[eventName] = handler;
            }
            else
            {
                eventHandlers[eventName] += handler; // 기존 핸들러에 추가
            }
        }

        /// <summary>
        /// 이벤트 등록 해제
        /// </summary>
        public void UnregisterEvent(string eventName, Action handler)
        {
            if (eventHandlers.ContainsKey(eventName))
            {
                eventHandlers[eventName] -= handler;

                // 핸들러가 모두 제거되면 키 삭제
                if (eventHandlers[eventName] == null)
                {
                    eventHandlers.Remove(eventName);
                }
            }
        }

        /// <summary>
        /// 애니메이션 이벤트 호출
        /// </summary>
        public void OnAnimationEvent(string eventName)
        {
            if (eventHandlers.ContainsKey(eventName))
            {
                eventHandlers[eventName]?.Invoke();
            }
            else
            {
                Debug.LogWarning($"No handlers for animation event: {eventName}");
            }
        }
    }

}
