using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class EventHandle<T> : ScriptableObject
    {
        public event Action<T> OnEvent;
        [SerializeField] private T _lastMessage = default;

        #region UNITY_EDITOR
#if UNITY_EDITOR
        [SerializeField] private bool _logOnDispatch = false;
#endif
        #endregion

        public void Invoke(T message)
        {
            _lastMessage = message;
            OnEvent?.Invoke(message);

            #region UNITY_EDITOR
#if UNITY_EDITOR
            if (_logOnDispatch)
                Debug.Log($"Event [{name}] dispatched");
#endif
            #endregion
        }

        public void Invoke(T message, GameObject dispatcher)
        {
            _lastMessage = message;
            OnEvent?.Invoke(message);

            #region UNITY_EDITOR
#if UNITY_EDITOR
            if (_logOnDispatch)
                Debug.Log($"Event [{name}] dispatched by [{dispatcher.name}]", dispatcher);
#endif
            #endregion
        }

        public void Invoke(T message, string debugName)
        {
            _lastMessage = message;
            OnEvent?.Invoke(message);

            #region UNITY_EDITOR
#if UNITY_EDITOR
            if (_logOnDispatch)
                Debug.Log($"Event [{name}] dispatched by [{debugName}]");
#endif
            #endregion
        }
    }
}