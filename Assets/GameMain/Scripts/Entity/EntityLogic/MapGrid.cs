using DG.Tweening;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class MapGrid : Entity
    {
        private MapGridData m_MapGridData;

        public Vector3 Position
        {
            get { return m_MapGridData.Position; }
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_MapGridData = (MapGridData) userData;
            if (m_MapGridData == null)
            {
                Log.Error("Entity data is invalid.");
                return;
            }

            CachedTransform.localScale = new Vector3(m_MapGridData.W, m_MapGridData.H, 1);

            for (int i = 1 - m_MapGridData.W / 2; i < m_MapGridData.W / 2; i++)
            {
                for (int j = 1 - m_MapGridData.H / 2; j < m_MapGridData.H / 2; j++)
                {
                    int w = i + (int) m_MapGridData.Position.x;
                    int h = j + (int) m_MapGridData.Position.y;
                    GameEntry.MapData[w, h] = new PassableObstacle(w, h);
                }
            }

            GameEntry.Event.Subscribe(RotateEventArg.EventId, OnRatate);
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            GameEntry.Event.Unsubscribe(RotateEventArg.EventId, OnRatate);
        }


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            CachedTransform.localRotation = m_MapGridData.Rotation;
            CachedTransform.localPosition = m_MapGridData.Position;
        }

        private void OnRatate(object sender, GameEventArgs e)
        {
            RotateEventArg ne = e as RotateEventArg;
            if (ne == null)
            {
                return;
            }

            DOTween.To(() => m_MapGridData.Rotation, x => m_MapGridData.Rotation = x,
                new Vector3(ne.IsTop ? 90 : 0, 0, 0), 0.5f);
            DOTween.To(() => m_MapGridData.Position, x => m_MapGridData.Position = x,
                new Vector3(m_MapGridData.Position.x, ne.IsTop ? m_MapGridData.ForwardY : m_MapGridData.TopY, 1), 0.5f);
        }
    }
}