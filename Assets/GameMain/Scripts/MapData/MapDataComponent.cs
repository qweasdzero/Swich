using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    /// <summary>
    /// 地图组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Map Data")]
    public sealed partial class MapDataComponent : GameFrameworkComponent
    {
        private Block[][] m_GridData;

        protected override void Awake()
        {
            base.Awake();
            m_GridData = new Block[Constant.Map.Wight][];
            for (int i = 0; i < Constant.Map.Wight; i++)
            {
                m_GridData[i] = new Block[Constant.Map.Height];
                for (int j = 0; j < Constant.Map.Height; j++)
                {
                    m_GridData[i][j] = new ProHibitObstacle(i - Constant.Map.HalfWidth, j - Constant.Map.HalfHight);
                }
            }
        }

        public Block this[int w, int h]
        {
            get
            {
                w += Constant.Map.HalfWidth;
                h += Constant.Map.HalfHight;

                return m_GridData[w][h];
            }
            set
            {
                w += Constant.Map.HalfWidth;
                h += Constant.Map.HalfHight;
                if (m_GridData[w] == null)
                {
                    m_GridData[w] = new Block[Constant.Map.Height];
                }

                m_GridData[w][h] = value;
            }
        }

//        public override string ToString()
//        {
//            System.Text.StringBuilder sb = new System.Text.StringBuilder();
//
//            for (int h = Constant.Map.HalfHight + 1; h >= -Constant.Map.HalfHight - 1; h--)
//            {
//                sb.Append("{");
//                for (int w = -Constant.Map.HalfWidth - 1; w <= Constant.Map.HalfWidth + 1; w++)
//                {
//                    sb.AppendFormat("{0},", (m_GridData[h][w]).ToString());
//                }
//
//                sb.Append("},").AppendLine();
//            }
//
//            return sb.ToString();
//        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (m_GridData == null)
            {
                return;
            }

            Gizmos.matrix = Matrix4x4.identity;


            foreach (var row in m_GridData)
            {
                foreach (var grid in row)
                {
                    if (grid is PassableObstacle)
                    {
                        Gizmos.color = Color.green;
                    }
                    else if (grid is ProHibitObstacle)
                    {
                        Gizmos.color = Color.red;
                    }

// else if (grid.Value is StaticObstacle)
//                    {
//                        Gizmos.color = Color.blue;   
//                    }
//                    else if (grid.Value is FakeBlock)
//                    {
//                        Gizmos.color = Color.black;
//                    }else if (grid.Value is DynamicObstacle)
//                    {
//                        Gizmos.color = Color.yellow;
//                    }
                    Gizmos.DrawWireCube(new Vector3(grid.W, grid.H), Vector3.one);
                }
            }
        }
#endif
    }
}