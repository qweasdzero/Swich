using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class PassableObstacle : Block
    {
        public PassableObstacle(int w, int h) : base(w, h)
        {
        }

        public override bool Passable
        {
            get { return true; }
        }

        public override bool Feasible(Vector3 enlargeRealPosition, Vector3 faceTo)
        {
            return Passable;
        }
    }
}