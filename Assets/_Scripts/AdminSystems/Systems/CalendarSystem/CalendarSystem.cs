using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CQM.Databases;
using CQM.Components;

namespace CQM.Systems
{
    public class CalendarSystem
    {
        private CalendarData _data;

        public void Initialize(CalendarData data, StoryDB storyBd)
        {
            _data = data;
        }

        public void AdvanceADay()
        {
            _data.m_Day += 1;
            ClampData();
        }

        private void ClampData()
        {
            if (_data.m_Day > 31)
            {
                _data.m_Day = 0;
                _data.m_Month += 1;
            }

            if (_data.m_Month > 12)
            {
                _data.m_Year += 1;
            }
        }
    }
}

namespace CQM.Components
{
    [System.Serializable]
    public class CalendarData
    {
        public int m_Year;
        public int m_Month;
        public int m_Day;
    }
}
