using GameFramework;

namespace StarForce
{
    public class MenuFormModel : UGuiFormModel<MenuForm, MenuFormModel>
    {
        public void GameStart()
        {
            Page.GameStart();
        }
    }

    public class MenuForm : UGuiFormPage<MenuForm, MenuFormModel>
    {
        public void GameStart()
        {
            GameEntry.Event.Fire(this, ReferencePool.Acquire<GameStartEventArg>().Fill());
        }
    }
}