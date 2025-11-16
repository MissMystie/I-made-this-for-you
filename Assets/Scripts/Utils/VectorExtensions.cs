using System.Collections.Generic;
using UnityEngine;

namespace Mystie
{
    public static class VectorExtensions
    {
        public static Vector2 XY(this Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        public static float Angle(this Vector2 v)
        {
            return Vector2.SignedAngle(Vector2.up, v);
        }

        public static float Angle(this Vector2 v, Vector2 w)
        {
            return Vector2.SignedAngle(w, v);
        }

        public static Vector2 Rotate(this Vector2 v, float degrees)
        {
            float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
            float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

            float tx = v.x;
            float ty = v.y;

            v.x = (cos * tx) - (sin * ty);
            v.y = (sin * tx) + (cos * ty);

            return v;
        }

        public static Vector2 Mirror(this Vector2 v)
        {
            return new Vector2(-v.x, -v.y);
        }

        public static Vector2 MirrorX(this Vector2 v)
        {
            return new Vector2(-v.x, v.y);
        }

        public static Vector2 mirrorY(this Vector2 v)
        {
            return new Vector2(v.x, -v.y);
        }

        public static List<Vector2> GetArc(this Vector2 dir, float arcLength, float radius, float maxSteps = 20)
        {
            List<Vector2> arcPoints = new List<Vector2>();

            float angle = -dir.Angle() - arcLength / 2;
            float stepAngle = arcLength / maxSteps;

            for (int i = 0; i <= maxSteps; i++)
            {
                float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
                float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

                arcPoints.Add(new Vector2(x, y));

                angle += stepAngle;
            }

            return arcPoints;
        }
    }
}
