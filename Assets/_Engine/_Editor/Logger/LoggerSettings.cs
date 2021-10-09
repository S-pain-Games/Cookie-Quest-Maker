using System.Collections.Generic;
using UnityEngine;

namespace Debugging
{
    [CreateAssetMenu()]
    public class LoggerSettings : ScriptableObject
    {
        public int minPriority = 0;

        public bool showPriority = true;
        public bool showFilter = true;

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