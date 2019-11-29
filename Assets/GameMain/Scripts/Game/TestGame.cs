using System.Collections.Generic;
using GameFramework;
using GameFramework.Event;
using UnityEngine;


namespace StarForce
{
    public struct MapInfo
    {
        public int PosX;
        public int TopY;
        public int ForwardY;
        public int W;

        public MapInfo(int posX, int topY, int forwardY, int w)
        {
            PosX = posX;
            TopY = topY;
            ForwardY = forwardY;
            W = w;
        }
    }

    public class TestGame : GameBase
    {
//        private float m_ElapseSeconds = 0f;

        public override GameMode GameMode
        {
            get { return GameMode.Test; }
        }

        private int m_NextX;
        private LinkedList<MapInfo> m_MapInformations;

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_NextX = -4;
            GameEntry.GameControl.Init();
            m_MapInformations = new LinkedList<MapInfo>();
            ShowMap(8, 8, 0, 0);
            ShowMap(4, 4, -6, -6);
            ShowMap(6, 6, -3, 5);
            ShowMap(4, 4, 2, -2);
            ShowMap(4, 6, 4, 4);
            ShowMap(6, 6, 0, 6);
            GameEntry.Entity.ShowPlayer(new PlayerData(GameEntry.Entity.GenerateSerialId(), 20)
            {
                MapInformations = m_MapInformations,
            });


            GameEntry.Event.Subscribe(GameOverEventArg.EventId, OnGameOver);
        }

        public override void Shutdown()
        {
            base.Shutdown();
            GameEntry.Event.Unsubscribe(GameOverEventArg.EventId, OnGameOver);
        }

        private void OnGameOver(object sender, GameEventArgs e)
        {
            GameOverEventArg ne = e as GameOverEventArg;
            if (ne == null)
            {
                return;
            }

            GameOver = true;
        }

        private void ShowMap(int w, int h, int y, int forwardY)
        {
            GameEntry.Entity.ShowMapGrid(new MapGridData(GameEntry.Entity.GenerateSerialId(), 10, w, h)
            {
                Position = new Vector3(m_NextX + w / 2, y, 1),
                TopY = y,
                ForwardY = forwardY,
            });
            m_MapInformations.AddLast(new MapInfo(m_NextX + w / 2, y, forwardY, w));
            m_NextX += w;
        }
    }
}