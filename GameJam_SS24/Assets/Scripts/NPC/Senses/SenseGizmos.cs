using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
/// <summary>
/// Helper class to draw custom gizmos for the senes (eyes, ears)
/// </summary>
public class SenseGizmos
{
    private static float lineWidth = 5f;
    private static Color colorRadius = new Color(1, 0.9215686f, 0.01568628f, 1);
    private static Color colorDisc = new Color(0.3357821f, 0, 1, 0.1294118f);
    private static Color colorArc = new Color(0, 0, 1, 0.5f);
    private static Color colorInverseArc = new Color(0, 0, 0, 0.2f);
    private static Color colorRayMissing = Color.magenta;
    private static Color colorRayHitting = Color.green;


    public static void DrawRangeCircle(Vector3 startPosition, Vector3 normal, float radius) 
    {
        Handles.color = colorRadius;
        Handles.DrawWireDisc(startPosition, normal, radius, lineWidth);
    }

    public static void DrawRangeDisc(Vector3 startPosition, Vector3 normal, float radius) 
    {
        Handles.color = colorDisc;
        Handles.DrawSolidDisc(startPosition, normal, radius);
    }

    public static void DrawFOV(Vector3 position, Vector3 forward, Vector3 normal, float range, float fov)
    {
        forward.y = 0;

        Handles.color = colorArc;
        Vector3 startFovArc = Quaternion.AngleAxis(-fov * 0.5f, Vector3.up) * forward;

        Handles.DrawSolidArc(position, normal, startFovArc, fov, range);

        Vector3 startInverseFovArc = Quaternion.AngleAxis(fov * 0.5f, Vector3.up) * forward;

        Handles.color = colorInverseArc;
        Handles.DrawSolidArc(position, normal, startInverseFovArc, 360f - fov, range);
    }

    public static void DrawRay(Vector3 from, Vector3 to, bool isHitting) 
    {
        Handles.color = isHitting ? colorRayHitting : colorRayMissing;
        Handles.DrawLine(from, to);
    }
}
#endif