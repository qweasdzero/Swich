using DG.Tweening;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class Player : Entity
    {
        [SerializeField] private PlayerData m_PlayerData;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_PlayerData = (PlayerData) userData;
            if (m_PlayerData == null)
            {
                Log.Error("Entity data is invalid.");
                return;
            }

            m_PlayerData.IsTop = true;
            m_PlayerData.MapInfo = m_PlayerData.MapInformations.First;

            GameEntry.Event.Subscribe(RotateEventArg.EventId, OnRatate);
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
            GameEntry.Event.Unsubscribe(RotateEventArg.EventId, OnRatate);
        }

        private void OnRatate(object sender, GameEventArgs e)
        {
            RotateEventArg ne = e as RotateEventArg;
            if (ne == null)
            {
                return;
            }

            if (ne.IsTop)
            {
                DOTween.To(() => m_PlayerData.Position, x => m_PlayerData.Position = x,
                    new Vector3(CachedTransform.localPosition.x, m_PlayerData.PosForwardY, 0), 0.5f);
                m_PlayerData.PosTopY = m_PlayerData.Position.y;
            }
            else
            {
                DOTween.To(() => m_PlayerData.Position, x => m_PlayerData.Position = x,
                    new Vector3(m_PlayerData.Position.x, m_PlayerData.PosTopY, 2), 0.5f);
                m_PlayerData.PosForwardY = m_PlayerData.MapInfo.Value.TopY + 1;
            }

            m_PlayerData.IsTop = !ne.IsTop;
        }

        public void TopMove()
        {
            m_PlayerData.PlayerRealPosition = new Vector3(Mathf.RoundToInt(m_PlayerData.Position.x),
                Mathf.RoundToInt(m_PlayerData.Position.y), Mathf.RoundToInt(m_PlayerData.Position.z));

            m_PlayerData.EnlargeRealPosition = new Vector3(
                (int) (m_PlayerData.Position.x * Constant.Map.EnLagerUnit),
                (int) (m_PlayerData.Position.y * Constant.Map.EnLagerUnit));

            int realX = (int) m_PlayerData.XRound;
            int realY = (int) m_PlayerData.YRound;


            int faceToX = (int) Input.GetAxisRaw("Horizontal");
            int faceToY = (int) Input.GetAxisRaw("Vertical");

            Vector3 faceto = new Vector2(faceToX, faceToY);

            Block centerGridInfo = GameEntry.MapData[realX + faceToX, realY + faceToY];
            Block centerGridInfo2 = GameEntry.MapData[realX + faceToX * 2, realY + faceToY * 2];

            bool centerbool = centerGridInfo.Feasible(m_PlayerData.EnlargeRealPosition, faceto);
            bool centerbool2 =
                centerGridInfo2.Feasible(m_PlayerData.EnlargeRealPosition + 2 * Constant.Map.EnLagerUnit * faceto,
                    faceto);

            if (centerbool)
            {
                m_PlayerData.Position += faceto * Constant.Map.SliderStep;
            }
            else
            {
                if (centerbool2)
                {
                    m_PlayerData.Position += faceto * Constant.Map.SliderStep;
                }
            }

            m_PlayerData.Position = new Vector3(
                Mathf.Clamp(m_PlayerData.Position.x, -Constant.Map.HalfWidth, Constant.Map.HalfWidth),
                Mathf.Clamp(m_PlayerData.Position.y, -Constant.Map.HalfHight, Constant.Map.HalfHight));
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (m_PlayerData.GameOver)
            {
                return;
            }

            if (m_PlayerData.Position.y < -8)
            {
                m_PlayerData.GameOver = true;
                GameEntry.Event.Fire(this, ReferencePool.Acquire<GameOverEventArg>().Fill());
                return;
            }

            if (m_PlayerData.IsTop)
            {
                for (int i = 0; i < m_PlayerData.Speed; i++)
                {
                    TopMove();
                }

                if (m_PlayerData.Position.x > (m_PlayerData.MapInfo.Value.PosX + m_PlayerData.MapInfo.Value.W / 2))
                {
                    m_PlayerData.MapInfo = m_PlayerData.MapInfo.Next;
                    m_PlayerData.PosForwardY = m_PlayerData.MapInfo.Value.ForwardY + 1;
                }

                if (m_PlayerData.Position.x < (m_PlayerData.MapInfo.Value.PosX - m_PlayerData.MapInfo.Value.W / 2))
                {
                    m_PlayerData.MapInfo = m_PlayerData.MapInfo.Previous;
                    m_PlayerData.PosForwardY = m_PlayerData.MapInfo.Value.ForwardY + 1;
                }
            }
            else
            {
                Move(realElapseSeconds);
            }


            GameEntry.Scene.MainCamera.transform.position = new Vector3(m_PlayerData.Position.x,
                m_PlayerData.Position.y, GameEntry.Scene.MainCamera.transform.position.z);

            CachedTransform.localPosition = m_PlayerData.Position;
            CachedTransform.localRotation = m_PlayerData.Rotation;
        }

        private void Move(float realElapseSeconds)
        {
            m_PlayerData.Position +=
                new Vector3(Input.GetAxisRaw("Horizontal"), 0) * m_PlayerData.Speed * Constant.Map.SliderStep;

            if (m_PlayerData.MapInfo == null)
            {
                m_PlayerData.Position += Vector3.down * realElapseSeconds * 10;
                return;
            }

            if (m_PlayerData.Position.x > m_PlayerData.MapInfo.Value.PosX + m_PlayerData.MapInfo.Value.W / 2)
            {
                m_PlayerData.MapInfo = m_PlayerData.MapInfo.Next;
            }
            else if (m_PlayerData.Position.x < m_PlayerData.MapInfo.Value.PosX - m_PlayerData.MapInfo.Value.W / 2)
            {
                m_PlayerData.MapInfo = m_PlayerData.MapInfo.Previous;
            }

            if (m_PlayerData.MapInfo == null)
            {
                return;
            }

            m_PlayerData.PosTopY = m_PlayerData.MapInfo.Value.TopY;

            if (m_PlayerData.Position.y > m_PlayerData.MapInfo.Value.ForwardY + 1 ||
                m_PlayerData.Position.y < m_PlayerData.MapInfo.Value.ForwardY)
            {
                m_PlayerData.Position += Vector3.down * realElapseSeconds * 10;
            }
        }
    }
}