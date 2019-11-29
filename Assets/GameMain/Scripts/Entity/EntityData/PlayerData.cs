using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarForce
{
    [SelectionBase]
    public class PlayerData : EntityData
    {
        public bool IsTop;

        [SerializeField] private Vector3 m_PlayerRealPosition = Vector3.zero;

        [SerializeField] private Vector3 m_EnlargeRealPosition = Vector3.zero;

        private float m_PosTopY;
        private float m_PosForwardY;
        private int m_Speed;
        private bool m_GameOver;
        public LinkedList<MapInfo> MapInformations;
        public LinkedListNode<MapInfo> MapInfo;
        
        public bool GameOver
        {
            get { return m_GameOver; }
            set { m_GameOver = value; }
        }

        public float PosTopY
        {
            get { return m_PosTopY; }
            set { m_PosTopY = value; }
        }

        public float PosForwardY
        {
            get { return m_PosForwardY; }
            set { m_PosForwardY = value; }
        }

        public int Speed
        {
            get { return m_Speed; }
            set { m_Speed = value; }
        }

        public Vector3 PlayerRealPosition
        {
            get { return m_PlayerRealPosition; }
            set { m_PlayerRealPosition = value; }
        }

        public Vector3 EnlargeRealPosition
        {
            get { return m_EnlargeRealPosition; }
            set { m_EnlargeRealPosition = value; }
        }

        public int XRound
        {
            get { return (int) PlayerRealPosition.x; }
        }

        public int YRound
        {
            get { return (int) PlayerRealPosition.y; }
        }

        public PlayerData(int entityId, int typeId) : base(entityId, typeId)
        {
            m_PosTopY = 0;
            m_PosForwardY = 1;
            m_Speed = 10;
        }
    }
}