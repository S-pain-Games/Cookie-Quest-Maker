using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Debugging
{
    public class DebugDraw : MonoBehaviour
    {
        public List<Rect> _rects = new List<Rect>();

        public static void DrawText(string text, Vector2 pos)
        {

        }

        public static void DrawPoint(Vector2 point, float size)
        {

        }

        public static void DrawBoxCollider(BoxCollider2D col)
        {
            Vector2 topLeft = new Vector2(col.transform.position.x + col.offset.x - col.size.x / 2.0f,
                                          col.transform.position.y + col.offset.y + col.size.y / 2.0f);
            Vector2 topRight = new Vector2(col.transform.position.x + col.offset.x + col.size.x / 2.0f,
                                           col.transform.position.y + col.offset.y + col.size.y / 2.0f);

            Vector2 botRight = new Vector2(col.transform.position.x + col.offset.x + col.size.x / 2.0f,
                                           col.transform.position.y + col.offset.y - col.size.y / 2.0f);

            Vector2 botLeft = new Vector2(col.transform.position.x + col.offset.x - col.size.x / 2.0f,
                                           col.transform.position.y + col.offset.y - col.size.y / 2.0f);

            Debug.DrawLine(topLeft, topRight);
            Debug.DrawLine(topRight, botRight);
            Debug.DrawLine(botRight, botLeft);
            Debug.DrawLine(botLeft, topLeft);
        }

        public static void DrawRect(Rect rect)
        {
            Vector2[] points =
                { new Vector2(rect.xMin, rect.yMin) ,
        new Vector2(rect.xMax, rect.yMin),
        new Vector2(rect.xMax, rect.yMax),
        new Vector2(rect.xMin, rect.yMax)};

            for (int i = 0; i < 3; i++)
            {
                Debug.DrawLine(points[i], points[i + 1]);
            }
            Debug.DrawLine(points[3], points[0]);
        }

        public static void DrawRect(Rect rect, Color color)
        {
            Vector2[] points =
                { new Vector2(rect.xMin, rect.yMin) ,
        new Vector2(rect.xMax, rect.yMin),
        new Vector2(rect.xMax, rect.yMax),
        new Vector2(rect.xMin, rect.yMax)};

            for (int i = 0; i < 3; i++)
            {
                Debug.DrawLine(points[i], points[i + 1], color);
            }
            Debug.DrawLine(points[3], points[0], color);
        }

        public static void DrawPath(Vector2[] points)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Debug.DrawLine(points[i], points[i + 1]);
            }
        }

        public static void DrawPath(Vector2[] points, Color color)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Debug.DrawLine(points[i], points[i + 1], color);
            }
        }

        public static void DrawPath(Vector2[] points, Color color, float duration)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Debug.DrawLine(points[i], points[i + 1], color, duration);
            }
        }

        public static void DrawPath(Vector2[] points, Color color, float duration, bool depthTest)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                Debug.DrawLine(points[i], points[i + 1], color, duration, depthTest);
            }
        }
    }
}