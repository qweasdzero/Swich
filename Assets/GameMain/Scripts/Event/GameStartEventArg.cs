using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;

namespace StarForce
{
    public class GameStartEventArg : GameEventArgs
    {
        public static readonly int EventId = typeof(GameStartEventArg).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }


        public override void Clear()
        {
        }

        public GameStartEventArg Fill()
        {
            return this;
        }
    }
}