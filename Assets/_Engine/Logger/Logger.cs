using Conditional = System.Diagnostics.ConditionalAttribute;
using System.Text;
using UnityEngine;
using UnityEditor;

namespace Debugging
{
    /// <summary>
    /// <para>Debug Logger that uses the <see cref="Diagnostics.ConditionalAttribute"/> to disable the log calls in RELEASE</para>
    /// <para>This allows the user to leave the debug calls in its code for future use without worrying them being called in the final build</para>
    /// </summary>
    [InitializeOnLoad]
    public static class Logger
    {
        private const string _compilationSymbol = "UNITY_EDITOR";
        private static LoggerSettings _settings;
        private static StringBuilder _sb = new StringBuilder();

        [Conditional(_compilationSymbol)]
        public static void Log(string message,
                               int priority = int.MaxValue,
                               string filterName = null)
        {
            bool filteredOut = CheckFilteredOut(filterName);

            if (priority >= _settings.minPriority && !filteredOut)
            {
                _sb.Clear();
                TryAppendPriorityAndFilter(ref _sb, priority, filterName);
                _sb.Append($" {message}");

                Debug.Log(_sb.ToString());
            }
        }

        [Conditional(_compilationSymbol)]
        public static void Log(string message,
                               string user,
                               int priority = int.MaxValue,
                               string filterName = null)
        {
            bool filteredOut = CheckFilteredOut(filterName);

            if (priority >= _settings.minPriority && !filteredOut)
            {
                _sb.Clear();
                _sb.Append($"[{user}]");
                TryAppendPriorityAndFilter(ref _sb, priority, filterName);
                _sb.Append($" {message}");

                Debug.Log(_sb.ToString());
            }
        }

        [Conditional(_compilationSymbol)]
        public static void Log(string message,
                               Object context,
                               int priority = int.MaxValue,
                               string filterName = null)
        {
            bool filteredOut = CheckFilteredOut(filterName);

            if (priority >= _settings.minPriority && !filteredOut)
            {
                _sb.Clear();
                TryAppendPriorityAndFilter(ref _sb, priority, filterName);
                _sb.Append($" {message}");

                Debug.Log(_sb.ToString(), context);
            }
        }

        static Logger()
        {
            LoadConfig();
        }

        private static void LoadConfig()
        {
            _settings = AssetDatabase.LoadAssetAtPath<LoggerSettings>("Assets/Engine/Logger/LoggerSettings.asset");
        }

        private static void TryAppendPriorityAndFilter(ref StringBuilder sb, int priority, string filterName)
        {
            if (_settings.showPriority)
                if (priority == int.MaxValue)
                    _sb.Append($"[P:MAX]");
                else
                    _sb.Append($"[P:{priority}]");

            if (_settings.showFilter && filterName.Length > 0)
                _sb.Append($"[F:{filterName}]");
        }

        private static bool CheckFilteredOut(string filterName)
        {
            if (_settings.useFilters && filterName != null)
                return _settings.filters.Find((f) => f.name == filterName).active;
            else
                return false;
        }
    }
}