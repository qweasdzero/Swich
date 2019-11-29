using UnityEngine;

namespace StarForce
{
    public class MapGridData : EntityData
    {
        private int m_W;
        private int m_H;
        public int TopY;
        public int ForwardY;
        
        public int W
        {
            get { return m_W; }
            set { m_W = value; }
        }

        public int H
        {
            get { return m_H; }
            set { m_H = value; }
        }

        public MapGridData(int entityId, int typeId,int w,int h) : base(entityId, typeId)
        {
            W = w;
            H = h;
        }
    }
}