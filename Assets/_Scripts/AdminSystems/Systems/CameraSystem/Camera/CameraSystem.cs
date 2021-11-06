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
        private Singleton_CameraDataComponent _camData;

        public void Initialize(Singleton_CameraDataComponent camData)
        {
            _camData = camData;
        }

        public void RegisterEvents(out ID sysID, out EventSys commands, out EventSys callbacks)
        {
            commands = new EventSys();
            callbacks = new EventSys();
            sysID = new ID("camera_sys");

            var evt = commands.AddEvent<Transform>(new ID("retarget_cmd"));
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
    public class Singleton_CameraDataComponent
    {
        public CinemachineVirtualCamera m_MainCam;
    }
}