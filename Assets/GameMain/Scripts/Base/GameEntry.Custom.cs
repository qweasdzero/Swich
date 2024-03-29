﻿//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2019 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using UnityEngine;

namespace StarForce
{
    /// <summary>
    /// 游戏入口。
    /// </summary>
    public partial class GameEntry : MonoBehaviour
    {
        public static BuiltinDataComponent BuiltinData
        {
            get;
            private set;
        }   
        
        public static MapDataComponent MapData
        {
            get;
            private set;
        }      
        
        public static GameControlComponent GameControl
        {
            get;
            private set;
        }

        private static void InitCustomComponents()
        {
            BuiltinData = UnityGameFramework.Runtime.GameEntry.GetComponent<BuiltinDataComponent>();
            MapData = UnityGameFramework.Runtime.GameEntry.GetComponent<MapDataComponent>();
            GameControl = UnityGameFramework.Runtime.GameEntry.GetComponent<GameControlComponent>();
        }
    }
}
