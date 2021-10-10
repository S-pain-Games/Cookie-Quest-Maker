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
    public static class Logg
    {
        public static LoggerSettings settings;

        private const string _compilationSymbol = "UNITY_EDITOR";
        private static StringBuilder _sb = new StringBuilder();

        [Conditional(_compilationSymbol)]
        public static void Log(string message,
                               int priority = int.MaxValue,
                               string filterName = null)
        {
            bool filteredOut = CheckFilteredOut(filterName);

            if (priority >= settings.minPriority && !filteredOut)
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

            if (priority >= settings.minPriority && !filteredOut)
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

            if (priority >= settings.minPriority && !filteredOut)
            {
                _sb.Clear();
                TryAppendPriorityAndFilter(ref _sb, priority, filterName);
                _sb.Append($" {message}");

                Debug.Log(_sb.ToString(), context);
            }
        }

        static Logg()
        {
            LoadConfig();
        }

        [ExecuteAlways]
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void LoadConfig()
        {
            settings = AssetDatabase.LoadAssetAtPath<LoggerSettings>("Assets/_Engine/Logger/LoggerSettings.asset");
            if (settings == null)
                Debug.LogError("Logger couldn't initialize because it didn't found the settings asset");
        }

        private static void TryAppendPriorityAndFilter(ref StringBuilder sb, int priority, string filterName)
        {
            if (settings.showPriority)
                if (priority == int.MaxValue)
                    _sb.Append($"[P:MAX]");
                else
                    _sb.Append($"[P:{priority}]");

            if (settings.showFilter && filterName != null)
                _sb.Append($"[F:{filterName}]");
        }

        private static bool CheckFilteredOut(string filterName)
        {
            if (settings.useFilters && filterName != null)
                return settings.filters.Find((f) => f.name == filterName).active;
            else
                return false;
        }
    }
}