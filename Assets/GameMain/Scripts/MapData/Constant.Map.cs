using UnityEngine;

namespace StarForce
{
    public static partial class Constant
    {
        public static class Map
        {
            public const int Wight = 161;
            public const int HalfWidth = 80;
            public const int Height = 81;
            public const int HalfHight = 40;
            public const float GridUnit = 1f;
            public const int EnLagerUnit = 250;
            public const float SliderStep = 1.0f / EnLagerUnit;
            public const int SmoothValue = 64;
            private static readonly Matrix4x4 s_Matrix = Matrix4x4.identity;

            /// <summary>
            /// 地图矩阵。
            /// </summary>
            public static Matrix4x4 Matrix
            {
                get { return s_Matrix; }
            }

            static Map()
            {
                s_Matrix.m00 *= GridUnit;
                s_Matrix.m11 *= GridUnit;
            }
        }
    }
}