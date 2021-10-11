using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    public class LoggerSettings
    {
        //public LogVerbosity minPriority = LogVerbosity.Medium;

        public bool showPriority = false;
        public bool showFilter = false;

        public bool useFilters = false;
        public List<LogFilter> filters = new List<LogFilter>();
    }

    [System.Serializable]
    public class LogFilter
    {
        public string name = "Unnamed Filter";
        public bool active = false;
    }
}