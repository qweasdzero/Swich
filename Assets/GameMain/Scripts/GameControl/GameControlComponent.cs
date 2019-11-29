using System.Collections;
using System.Collections.Generic;
using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    /// <summary>
    /// 游戏控住组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Game Control")]
    public class GameControlComponent : GameFrameworkComponent
    {
        private bool m_IsTop;
        private float m_SwichTime;

        public void Init()
        {
            m_IsTop = true;
            m_SwichTime = 1;
        }

        void Update()
        {
            if (m_SwichTime > 0)
            {
                m_SwichTime -= Time.deltaTime;
            }

            if (m_SwichTime < 0 && Input.GetButton("Jump")) //旋转
            {
                GameEntry.Event.Fire(this, ReferencePool.Acquire<RotateEventArg>().Fill(m_IsTop));
                m_IsTop = !m_IsTop;
                m_SwichTime = 1f;
            }
        }
    }
}