//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2019 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace StarForce
{
    public class ProcedureMenu : ProcedureBase
    {
        private bool m_StartGame = false;
        private UGuiFormPage m_MenuForm = null;

        public override bool UseNativeDialog
        {
            get { return false; }
        }

        public void StartGame()
        {
            m_StartGame = true;
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Subscribe(GameStartEventArg.EventId, OnGameStart);

            m_StartGame = false;
            bool enteronmain = procedureOwner.GetData<VarBool>(Constant.ProcedureData.EnterOnMain);
            if (enteronmain)
            {
                GameEntry.UI.OpenUIForm(UIFormId.GameOverPage, this);
            }
            else
            {
                GameEntry.UI.OpenUIForm(UIFormId.MenuForm, this);
            }
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);
            GameEntry.Event.Unsubscribe(GameStartEventArg.EventId, OnGameStart);

            if (m_MenuForm != null)
            {
                m_MenuForm.Close(isShutdown);
                m_MenuForm = null;
            }
        }

        private void OnGameStart(object sender, GameEventArgs e)
        {
            GameStartEventArg ne = e as GameStartEventArg;
            if (ne == null)
            {
                return;
            }

            m_StartGame = true;
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_StartGame)
            {
                procedureOwner.SetData<VarInt>(Constant.ProcedureData.NextSceneId,
                    GameEntry.Config.GetInt("Scene.Main"));
                procedureOwner.SetData<VarInt>(Constant.ProcedureData.GameMode, (int) GameMode.Test);
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs) e;
            if (ne.UserData != this)
            {
                return;
            }

            m_MenuForm = (UGuiFormPage) ne.UIForm.Logic;
        }
    }
}