using UnityEngine;

namespace Utilities
{
    public static class Utilities
    {
        public static Vector2 InterpolateVector2(Vector2 start, Vector2 end, float maxTime, float elapsedTime)
        {
            return new Vector2
            (
                start.x + (((end.x - start.x) / maxTime) * elapsedTime),
                start.y + (((end.y - start.y) / maxTime) * elapsedTime)
            );
        }

        public static Vector3 InterpolateVector3(Vector3 start, Vector3 end, float maxTime, float elapsedTime)
        {
            return new Vector3
            (
                start.x + (((end.x - start.x) / maxTime) * elapsedTime),
                start.y + (((end.y - start.y) / maxTime) * elapsedTime),
                start.z + (((end.z - start.z) / maxTime) * elapsedTime)
            );
        }

        public static Color32 InterpolateColor32(Color32 start, Color32 end, float maxTime, float elapsedTime)
        {
            return new Color32
            (
                (byte)(start.r + (((end.r - start.r) / maxTime) * elapsedTime)),
                (byte)(start.g + (((end.g - start.g) / maxTime) * elapsedTime)),
                (byte)(start.b + (((end.b - start.b) / maxTime) * elapsedTime)),
                (byte)(start.a + (((end.a - start.a) / maxTime) * elapsedTime))
            );
        }

        public static void RemoveAllChildObjects(Transform parent)
        {
            while (parent.childCount > 0)
                Object.DestroyImmediate(parent.GetChild(0).gameObject);
        }
    }
}
