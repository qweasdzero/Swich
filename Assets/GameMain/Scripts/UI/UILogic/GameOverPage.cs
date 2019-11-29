using GameFramework;

namespace StarForce
{
    public class GameOverModel : UGuiFormModel<GameOverPage, GameOverModel>
    {
        public void GameStart()
        {
            Page.GameStart();
        }
    }

    public class GameOverPage : UGuiFormPage<GameOverPage, GameOverModel>
    {
        public void GameStart()
        {
            GameEntry.Event.Fire(this, ReferencePool.Acquire<GameStartEventArg>().Fill());
        }
    }
}