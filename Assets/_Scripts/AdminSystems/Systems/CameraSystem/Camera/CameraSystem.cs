using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;
using CQM.Components;

namespace CQM.Systems
{
    public class CameraSystem : ISystemEvents
    {
        private CameraDataComponent _camData;

        public void Initialize(CameraDataComponent camData)
        {
            _camData = camData;
        }

        public void RegisterEvents(out int sysID, out EventSys commands, out EventSys callbacks)
        {
            commands = new EventSys();
            callbacks = new EventSys();
            sysID = "camera_sys".GetHashCode();

            var evt = commands.AddEvent<Transform>("retarget_cmd".GetHashCode());
            evt.OnInvoked += Retarget;
        }

        private void Retarget(Transform target)
        {
            _camData.m_MainCam.Follow = target;
        }
    }
}

namespace CQM.Components
{

    [Serializable]
    public class CameraDataComponent
    {
        public CinemachineVirtualCamera m_MainCam;
    }
}