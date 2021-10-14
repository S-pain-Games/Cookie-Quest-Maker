using Conditional = System.Diagnostics.ConditionalAttribute;
using System.Text;
using UnityEngine;
using UnityEditor;


namespace UnityEngine
{
    /// <summary>
    /// <para>Debug Logger that uses the <see cref="Diagnostics.ConditionalAttribute"/> to disable the log calls in RELEASE</para>
    /// <para>This allows the user to leave the debug calls in its code for future use without worrying them being called in the final build</para>
    /// </summary>
    public static class Logg
    {
        public static LoggerSettings settings = new LoggerSettings();

        private const string _compilationSymbol = "UNITY_EDITOR";
        private static StringBuilder _sb = new StringBuilder();

        [Conditional(_compilationSymbol)]
        public static void Log(string message)
        {
            _sb.Clear();
            _sb.Append($" {message}");
            Debug.Log(_sb.ToString());
        }

        [Conditional(_compilationSymbol)]
        public static void Log(string message, string user)
        {
            _sb.Clear();
            _sb.Append($"[{user}]");
            _sb.Append($" {message}");
            Debug.Log(_sb.ToString());
        }

        [Conditional(_compilationSymbol)]
        public static void Log(string message, GameObject context)
        {
            _sb.Clear();
            _sb.Append($"[{context.gameObject.name}]");
            _sb.Append($" {message}");
            Debug.Log(_sb.ToString(), context);
        }


        [Conditional(_compilationSymbol)]
        public static void Log(string message, string user, GameObject context)
        {
            _sb.Clear();
            _sb.Append($"[{user}]");
            _sb.Append($" {message}");
            Debug.Log(_sb.ToString(), context);
        }

        private static void AppendExtras(ref StringBuilder sb, int priority, string filterName)
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