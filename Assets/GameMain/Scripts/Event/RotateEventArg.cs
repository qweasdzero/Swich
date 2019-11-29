using System.Collections;
using System.Collections.Generic;
using GameFramework.Event;
using UnityEngine;

namespace StarForce
{
    public class RotateEventArg : GameEventArgs
    {
        public static readonly int EventId = typeof(RotateEventArg).GetHashCode();

        public override int Id
        {
            get { return EventId; }
        }

        public bool IsTop;

        public override void Clear()
        {
        }

        public RotateEventArg Fill(bool istop)
        {
            IsTop = istop;
            return this;
        }
    }
}