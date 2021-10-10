using System;
using System.Collections.Generic;
using UnityEngine;

namespace Events
{
    public class EventHandle<T> : ScriptableObject
    {
        public event Action<T> OnEvent;
        [SerializeField] private T _lastMessage = default;

        [SerializeField] private List<GameObject> _dispatchers;
        [SerializeField] private List<GameObject> _recievers;

        #region UNITY_EDITOR
#if UNITY_EDITOR
        [SerializeField] private bool _logOnDispatch = false;
        [SerializeField] private bool _logOnRecieve = false;
#endif
        #endregion

        public void Dispatch(T message, GameObject dispatcher)
        {
            _lastMessage = message;
            OnEvent?.Invoke(message);

            #region UNITY_EDITOR
#if UNITY_EDITOR
            if (_logOnDispatch)
                Debug.Log($"Event [{name}] dispatched by [{dispatcher.name}]", dispatcher);
            if (_logOnRecieve)
                LogRecievers(dispatcher.name);
#endif
            #endregion
        }

        public void Dispatch(T message, string debugName)
        {
            _lastMessage = message;
            OnEvent?.Invoke(message);

            #region UNITY_EDITOR
#if UNITY_EDITOR
            if (_logOnDispatch)
                Debug.Log($"Event [{name}] dispatched by [{debugName}]");
            if (_logOnRecieve)
                LogRecievers(debugName);
#endif
            #endregion
        }

        /// <summary>
        /// Only useful for debugging purposes
        /// </summary>
        public void AddDispatcher(GameObject dispatcher)
        {
            if (!_dispatchers.Contains(dispatcher))
                _dispatchers.Add(dispatcher);
        }

        public void RemoveDispatcher(GameObject dispatcher)
        {
            _dispatchers.Remove(dispatcher);
        }

        /// <summary>
        /// Only useful for debugging purposes
        /// </summary>
        public void AddReciever(GameObject reciever)
        {
            if (!_recievers.Contains(reciever))
                _recievers.Add(reciever);
        }

        public void RemoveReciever(GameObject reciever)
        {
            _recievers.Remove(reciever);
        }

        private void OnEnable()
        {
            _dispatchers = new List<GameObject>();
            _recievers = new List<GameObject>();
        }

        private void LogRecievers(string dispacherName)
        {
            foreach (var reciever in _recievers)
            {
                Debug.Log($"Event [{name}] Recieved By [{reciever.name}] from [{dispacherName}]", reciever);
            }
        }
    }
}