using GameFramework.DataTable;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace StarForce
{
    public class ProHibitObstacle : Block
    {
        public ProHibitObstacle(int w, int h) : base(w, h)
        {
        }

        public override bool Passable
        {
            get { return false; }
        }


        public override bool Feasible(Vector3 enlargeRealPosition, Vector3 faceTo)
        {
            Rect rect = Rect.zero;
            rect.size = Vector2.one * (Constant.Map.EnLagerUnit * 2 - 249);
            rect.center = new Vector2(W * Constant.Map.EnLagerUnit, H * Constant.Map.EnLagerUnit);

            return !rect.Contains(enlargeRealPosition + 5 * faceTo);
        }
    }
}