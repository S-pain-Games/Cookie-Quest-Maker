using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Events
{
    [CreateAssetMenu(menuName = "Event System/Void Event Handle")]
    public class VoidEventHandle : ScriptableObject
    {
        public event Action OnEvent;

        #region UNITY_EDITOR
#if UNITY_EDITOR
        [SerializeField] private bool _logOnDispatch = false;
        [SerializeField] private bool _logOnRecieve = false;
#endif
        #endregion

        public void Invoke(GameObject dispatcher)
        {
            OnEvent?.Invoke();

            #region UNITY_EDITOR
#if UNITY_EDITOR
            if (_logOnDispatch)
                UnityEngine.Debug.Log($"Event [{name}] dispatched by [{dispatcher.name}]", dispatcher);
            if (_logOnRecieve)
                LogRecievers(dispatcher.name);
#endif
            #endregion
        }

        public void Dispatch(string debugName)
        {
            OnEvent?.Invoke();

            #region UNITY_EDITOR
#if UNITY_EDITOR
            if (_logOnDispatch)
                UnityEngine.Debug.Log($"Event [{name}] dispatched by [{debugName}]");
            if (_logOnRecieve)
                LogRecievers(debugName);
#endif
            #endregion
        }

        private void LogRecievers(string dispacherName)
        {
            var invocationList = OnEvent.GetInvocationList();
            foreach (Delegate d in invocationList)
            {
                MonoBehaviour m = d.Target as MonoBehaviour;
                if (m != null)
                {
                    UnityEngine.Debug.Log($"Event [{name}] Recieved By [{m.gameObject.name}] from [{dispacherName}]", m.gameObject);
                }
            }
        }
    }
}