using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;

namespace StarForce
{
    public class GameOverEventArg : GameEventArgs
    {
        public static readonly int EventId = typeof(GameOverEventArg).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }


        public override void Clear()
        {
        }

        public GameOverEventArg Fill()
        {
            return this;
        }
    }
}