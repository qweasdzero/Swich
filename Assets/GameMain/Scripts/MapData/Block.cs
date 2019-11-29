using System.Collections.Generic;
using GameFramework;
using UnityEngine;

namespace StarForce
{
    /// <summary>
    /// 方块。
    /// </summary>
    public abstract class Block
    {
        private int m_H;

        private int m_W;

        /// <summary>
        /// 获取或者设置宽度偏差。
        /// </summary>
        public int W
        {
            get { return m_W; }
            set { m_W = value; }
        }

        /// <summary>
        /// 获取或者设置高度偏差。
        /// </summary>
        public int H
        {
            get { return m_H; }
            set { m_H = value; }
        }

        /// <summary>
        /// 获取是否可连通。
        /// </summary>
        public abstract bool Passable { get; }

        protected Block(int w, int h)
        {
            m_W = w;
            m_H = h;
        }

        public abstract bool Feasible(Vector3 enlargeRealPosition, Vector3 faceTo);

        public Block this[int w, int h]
        {
            get { return GameEntry.MapData[w, h]; }
        }
    }
}