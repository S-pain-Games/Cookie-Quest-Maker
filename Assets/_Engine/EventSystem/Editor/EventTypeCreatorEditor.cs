using System.IO;
using UnityEngine;
using UnityEditor;

namespace EventSystem
{
    public class EventTypeCreatorEditor : EditorWindow
    {
        private string _eventName = "";
        private string _eventType = "";
        private string _basePath = "";

        [MenuItem("Tools/Event Type Creator")]
        private static void Init()
        {
            var window = GetWindow<EventTypeCreatorEditor>();
            window.titleContent.text = "Event Type Creator";
            window.minSize = new Vector2(200, 155);
            window.maxSize = new Vector2(200, 155);
            window.Show();
        }

        private void OnEnable()
        {
            // Get Script Containing Folder
            _basePath = PathUtilities.GetPath(this);
        }

        private void OnGUI()
        {
            EditorGUILayout.PrefixLabel("Event Name");
            _eventName = EditorGUILayout.TextField(_eventName);
            EditorGUILayout.PrefixLabel("Event Type");
            _eventType = EditorGUILayout.TextField(_eventType);

            GUILayout.Space(5);
            if (GUILayout.Button("Create Event Type"))
            {
                CreateEventSourceFile();
            }
            GUILayout.Space(5);
            EditorGUILayout.LabelField("Final File Name");
            EditorGUILayout.LabelField($"{_eventName}EventHandle.cs");
        }

        public void CreateEventSourceFile()
        {
            CreateHandle();
            AssetDatabase.Refresh();
        }

        private void CreateHandle()
        {
            string source = @"using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(menuName = {menuName})]
    public class {eventName}EventHandle : EventHandle<{eventType}>
    {

    }
}
";
            source = source.Replace("{eventType}", _eventType);
            source = source.Replace("{eventName}", _eventName);
            source = source.Replace("{menuName}", $"\"Event System/{_eventName} Event\"");

            File.WriteAllText($"{_basePath}{_eventName}EventHandle.cs", source);
        }
    }
}